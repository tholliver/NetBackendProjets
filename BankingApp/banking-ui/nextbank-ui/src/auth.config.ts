import type { NextAuthConfig } from 'next-auth';

export const authConfig = {
    pages: {
        signIn: '/login',
    },
    callbacks: {
        authorized({ auth, request: { nextUrl } }) {
            const isLoggedIn = !!auth?.user;
            const protectedPaths = ['/settings', '/profile', '/customer', '/transaction'];

            const isOnProtectedPath = protectedPaths.some((path) => nextUrl.pathname.startsWith(path));

            if (isOnProtectedPath) {
                if (isLoggedIn) return true;
                return false; // Redirect unauthenticated users to login page
            } else if (isLoggedIn) {
                return Response.redirect(new URL('/customer', nextUrl));
            }
            return true;
        },
    },
    providers: [],
} satisfies NextAuthConfig;