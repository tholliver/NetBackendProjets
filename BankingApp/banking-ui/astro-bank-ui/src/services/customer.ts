import type { Customer, ICredentials, IAuthToken, InfoCustomer, Account } from "@/types";
import { fetcher, authPuller, puller } from "./utils";


const BACKEND_URI = import.meta.env.BACKEND_URI

export async function fetchCustomers(pagination: number = 5) {
    return fetcher<Customer>('/api/customers/all')
}

// export async function fetchAuthCustomer(token: string) {
//     return await authPuller<InfoCustomer>(`${process.env.BACKEND_URI}/info`, token)
// }

// export async function getToken(credentials: ICredentials) {
//     return puller<ICredentials, IAuthToken>(`${process.env.BACKEND_URI}/login`, credentials)
// }

export async function getToken(credentials: ICredentials) {
    try {
        return await puller<ICredentials, IAuthToken>(`${BACKEND_URI}/login`, credentials);
    } catch (error) {
        console.error('Failed to get token:', error);
        throw new Error('Authentication failed. Please check your credentials.');
    }
}

export async function fetchAuthCustomer(token: string) {
    try {
        return await authPuller<InfoCustomer>(`${BACKEND_URI}/info`, token);
    } catch (error) {
        console.error('Failed to fetch customer info:', error);
        throw new Error('Failed to fetch customer information.');
    }
}

export async function fetchCustomerAccounts(token: string, id: string) {
    try {
        return await authPuller<Account[]>(`${BACKEND_URI}/accounts/${id}`, token);
    } catch (error) {
        console.error('Failed to fetch CUSTOMER ACCOUNTS info:', error);
        throw new Error('Failed to fetch CUSTOMER ACCOUNTS information.');
    }
}

export async function fetchCustomerAccountDetails(token: string, customerId: string, id: string) {
    try {
        return await authPuller<Account>(`${BACKEND_URI}/accounts/${customerId}/${id}`, token);
    } catch (error) {
        // console.error('Failed to fetch CUSTOMER ACCOUNT info:', error);
        throw new Error('Failed to fetch CUSTOMER ACCOUNT information.');
    }
}