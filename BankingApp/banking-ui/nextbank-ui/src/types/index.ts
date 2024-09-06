// Auth 

export interface ICredentials {
    email: string
    password: string
}

export interface IAuthToken {
    tokenType: string;
    accessToken: string;
    expiresIn: number;
    refreshToken: string;
}

// Customers 
export interface InfoCustomer {
    customerId: string
    firstName: string
    lastName: string
    phoneNumber: string
    email: string
    accessToken: string;
    refreshToken: string;
}

export interface Customer {
    name: string
    lastname: string
    email: string
    password: string
}

// Trnasactions

export interface Transaction {
    transactionId?: number;
    sourceAccountNumber: string;
    targetAccountNumber: string;
    status: number;
    amount: number;
    createdDate: string;
    processedDate: string;
    description: string;
    transactionType: number;
    accountId?: number;
}

export interface Account {
    accountId?: number;
    accountNumber: string;
    type: string;
    description: string;
    currency: string;
    interestRate: number;
    minimumBalance: number;
    allowedTransactions: number;
    earlyWithdrawalPenalty: boolean;
    customerId?: string;
    customer?: any | null;
    transactions?: Transaction[];
}

// Error types

export interface ErrorResponse {
    type: string;
    title: string;
    status: number;
    detail: string; // 'Failed' can be the default value, but keeping it as string allows for flexibility
}

export interface IZodError {
    code: string
    validation: string
    message: string
    path: string[]
}

// Sign up registration
// interface Account {
//     accountNumber: string;
//     type: string;
//     description: string;
//     currency: string;
//     interestRate: number;
//     minimumBalance: number;
//     allowedTransactions: number;
//     earlyWithdrawalPenalty: boolean;
// }

export interface IUserForm {
    FirstName: string;
    LastName: string;
    Email: string;
    Phone: string;
    Password: string;
    Accounts: Account[];
}
