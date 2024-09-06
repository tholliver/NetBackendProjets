'use server'

import { signIn, signOut } from '@/auth'
import { ICredentials } from '@/types';
import { AuthError } from 'next-auth'

export async function authenticate(
    formData: ICredentials,
) {
    try {
        const signInresponse = await signIn('credentials', formData);
        console.log('--------------------------------LOG IN RETURN ', signInresponse);

    } catch (error) {
        console.log('*-*-*-***********************', error, 'Hre', typeof (error));
        if (error instanceof AuthError) {
            // switch (error.type) {
            //     case 'CredentialsSignin' || 'CallbackRouteError':
            //         return 'Invalid credentials.';
            //     default:
            //         return 'Something went wrong.';
            // }
            if (['CredentialsSignin', 'CallbackRouteError'].includes(error.type)) {
                return 'Invalid credentials.';
            }
            return 'Something went wrong.';
        }
        throw error;
    }
}

export async function signOutCustomer() {
    await signOut()
}