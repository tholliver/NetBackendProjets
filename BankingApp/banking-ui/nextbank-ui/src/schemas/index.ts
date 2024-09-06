import { AccountType } from '@/models';
import { z } from 'zod'

export const customerSchema = z.object({
    FirstName: z.string().min(2, { message: "First name is required" }),
    LastName: z.string().min(2, { message: "Last name is required" }),
    Email: z.string().email({ message: "Invalid email address" }),
    Phone: z.string().regex(/^\+?[1-9]\d{1,14}$/, { message: "Invalid phone number" }),
    Password: z.string().min(8, { message: "Password must be at least 8 characters long" }),
    Accounts: z.array(
        z.object({
            accountNumber: z.coerce.string().min(1, { message: "Must provide a account number" }),
            type: z.nativeEnum(AccountType, { errorMap: () => ({ message: "Account type is required" }) }),
            description: z.string().min(1, { message: "Description is required" }),
            currency: z.string().min(1, { message: "Description is required" }),
            interestRate: z.coerce.number().positive({ message: "Interest rate must be a positive number" }),
            minimumBalance: z.coerce.number().positive({ message: "Minimum balance must be a positive number" }),
            allowedTransactions: z.coerce.number().optional(),
            earlyWithdrawalPenalty: z.boolean().refine(value => value === true, "You must agree to the terms and conditions"),
        })
    )
    // terms: z.boolean().refine((value) => value === true, { message: "You must agree to the terms and conditions" }),
});

export type SignUpSchemaType = z.infer<typeof customerSchema>


export const loginSchema = z.object({
    email: z.string().email(),
    password: z.string().min(8),
})

export type User = z.infer<typeof loginSchema>


export const transactionSchema = z.object({
    // TransactionId: z.number().int().nonnegative(),
    SourceAccountNumber: z.string(),
    TargetAccountNumber: z.string(),
    Status: z.number().int(),
    Amount: z.coerce.number(),
    CreatedDate: z.date(),
    ProcessedDate: z.date(),
    Description: z.string().optional().or(z.literal('')),
    TransactionType: z.number().int(),
    AccountId: z.number().int().nonnegative(),
})

export type TransactionV = z.infer<typeof transactionSchema>