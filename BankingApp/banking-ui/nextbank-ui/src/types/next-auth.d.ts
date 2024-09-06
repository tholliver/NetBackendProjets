import NextAuth from "next-auth"
import { InfoCustomer, IAuthToken } from "."

declare module "next-auth" {
    /**
     * Returned by `useSession`, `getSession` and received as a prop on the `SessionProvider` React Context
     */
    interface Session {
        user: InfoCustomer
    }

    interface User {
        customerId: string
        email: string;
        firstName: string;
        lastName: string;
        jwt: IAuthToken
    }
}