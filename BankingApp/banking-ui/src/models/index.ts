export type CustomerType = {
    FirstName: string;
    LastName: string;
    Email: string;
    Phone: string;
    Password: string;
    Accounts: Account[]; // Changed to an array of Account objects
};

export type CustomerTypo = {
    FirstName: string;
    LastName: string;
    Email: string;
    Phone: string;
    Password: string;
    // Accounts: Account[]; // Changed to an array of Account objects
};

export enum AccountType {
    CheckingAccount = 'Checking Account',
    SavingsAccount = 'Savings Account',
    MoneyMarketAccount = 'Money Market Account',
    CertificateOfDeposit = 'Certificate of Deposit'
}

export type Account = {
    accountNumber: string;
    type: AccountType;
    description: string;
    currency: string;
    interestRate: number;              // Annual interest rate in percentage
    minimumBalance: number;            // Minimum balance required in the corresponding currency
    allowedTransactions?: number;      // Number of allowed transactions per month (optional)
    earlyWithdrawalPenalty?: boolean;  // Indicates if there is a penalty for early withdrawal (optional)
};