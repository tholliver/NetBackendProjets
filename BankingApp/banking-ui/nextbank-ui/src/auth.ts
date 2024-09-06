import NextAuth from 'next-auth';
import { authConfig } from './auth.config';
import Credentials from 'next-auth/providers/credentials'
import { z } from 'zod'
import { fetchAuthCustomer, getToken } from './services/customers';
import { IAuthToken } from './types';


export const { auth, signIn, signOut } = NextAuth({
    ...authConfig,
    providers: [
        Credentials({
            async authorize(credentials) {
                const parsedCredentials = z.object({
                    email: z.string().email(),
                    password: z.string().min(6),
                }).safeParse(credentials);

                if (parsedCredentials.success) {
                    const { email, password } = parsedCredentials.data;

                    try {
                        const token = await getToken({ email, password });
                        const customer = await fetchAuthCustomer(token.accessToken);
                        // console.log('++++++++++++++++++++++++Here on customer: ', customer);

                        if (customer) {
                            return {
                                customerId: customer.customerId,
                                email: customer.email,
                                firstName: customer.firstName,
                                lastName: customer.lastName,
                                jwt: token,
                            };
                        } else {
                            throw new Error('Failed to retrieve customer information.');
                        }
                    } catch (error) {
                        console.error('Authorization error:', error);
                        throw new Error('Authorization failed');
                    }
                }

                throw new Error('Invalid credentials');
            },
        }),

    ],
    callbacks: {
        redirect: async ({ url, baseUrl }) => {
            return baseUrl;
        },
        jwt: async ({ token, user }) => {
            // console.log('Here on the batch', token);
            if (user) {
                token.firstName = user.firstName
                token.lastName = user.lastName
                token.email = user.email
                token.customerId = user.customerId
                token.jwt = user.jwt
            }
            return token;

            // if (token) {
            //     token
            //     return {
            //         accessToken: token.user?.token?.accessToken,
            //         accessTokenExpires: Date.now() + token.user?.token?.expiresIn,
            //         refreshToken: token.user?.token?.refreshToken,
            //     }
            // }

            // // Return previous token if the access token has not expired yet
            // if (Date.now() < token?.accessTokenExpires) {
            //     return token
            // }

            // // Access token has expired, try to update it
            // console.log('AT THE END WE OGING TO REFRESH', token);
            // const resfresedToken = await refreshAccessToken(token.user?.token)
            // return resfresedToken
        },
        session: async ({ session, token }) => {
            // console.log('On the SESSION CALLBACK', user);
            const { accessToken, refreshToken } = (token.jwt as { accessToken: string; refreshToken: string })

            session.user.customerId = `${token.customerId}`;
            session.user.firstName = `${token.firstName}`;
            session.user.lastName = `${token.lastName}`;
            session.user.email = token.email!;
            session.user.accessToken = accessToken;
            session.user.refreshToken = refreshToken
            return session;
        }
    }
});


async function refreshAccessToken(token: IAuthToken) {
    console.log('************TOken: ', token)
    try {

        const response = await fetch(`${process.env.BACKEND_URI}/refresh`, {
            method: "POST",
            body: JSON.stringify({
                accessToken: token.accessToken,
                refreshToken: token.refreshToken,
            }),
            headers: {
                "Content-Type": "application/json",
            },
        });

        const refreshedTokens: IAuthToken = await response.json();
        console.log('------------THE REFRESH RESPONSE: ', refreshedTokens);

        if (!response.ok) {
            throw refreshedTokens;
        }

        return {
            ...token,
            accessToken: refreshedTokens.accessToken,
            expiresIn: Date.now() + refreshedTokens.expiresIn * 1000,
            refreshToken: refreshedTokens.refreshToken ?? token.refreshToken, // Fall back to old refresh token
        };
    } catch (error) {
        console.error("Error refreshing access token", error);
        return {
            ...token,
            error: "RefreshAccessTokenError",
        };
    }
}

