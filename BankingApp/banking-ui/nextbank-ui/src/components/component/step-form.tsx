'use client'

import { useState } from 'react'
import {
  Card,
  CardHeader,
  CardTitle,
  CardDescription,
  CardContent,
} from '@/components/ui/card'
import { Label } from '@/components/ui/label'

import { Button } from '@/components/ui/button'
import { CreditCardIcon, DollarSignIcon, WalletCardsIcon } from './election'
import { RadioGroup, RadioGroupItem } from '../ui/radio-group'
import { Form } from '../forms/Formix'
import { AccountType, CustomerType } from '@/models'
import { customerSchema } from '@/schemas'
import { Controller } from 'react-hook-form'

export function StepForm() {
  const [step, setStep] = useState(1)

  const submit = (data: CustomerType) => {
    console.log('this shit working', data)
  }

  return (
    <Form<CustomerType> onSubmit={submit} schema={customerSchema}>
      {({ register, getValues, formState: { errors } }) => (
        <>
          <Card className="mx-auto max-w-2xl">
            <CardHeader>
              <CardTitle>Open an Election Account</CardTitle>
              <CardDescription>
                Set up your election-related banking account with ease.
              </CardDescription>
            </CardHeader>
            <CardContent>
              {step === 1 && (
                <div className="grid gap-6">
                  <Controller
                    name=""
                    render={({ field: { onChange, onBlur, value, ref } }) => (
                      <RadioGroup
                        className="grid grid-cols-3 gap-4"
                        onValueChange={onChange}
                        onClick={() => {
                          console.log(getValues('Accounts.0.type'))
                        }}
                      >
                        <Label
                          htmlFor="CheckingAccount"
                          className={`border cursor-pointer rounded-md p-4 flex flex-col items-center justify-between hover:bg-accent hover:text-accent-foreground [&:has([data-state=checked])]:border-primary`}
                        >
                          <RadioGroupItem
                            {...register('Accounts.0.type', { required: true })}
                            value={AccountType.CheckingAccount}
                            id="CheckingAccount"
                            className="peer sr-only"
                          />
                          <CreditCardIcon className="mb-3 h-6 w-6" />
                          Checking Account
                        </Label>
                        <Label
                          htmlFor="SavingsAccount"
                          className={`border cursor-pointer rounded-md p-4 flex flex-col items-center justify-between hover:bg-accent hover:text-accent-foreground [&:has([data-state=checked])]:border-primary`}
                        >
                          <RadioGroupItem
                            {...register('Accounts.0.type', { required: true })}
                            value={AccountType.SavingsAccount}
                            id="SavingsAccount"
                            className="peer sr-only"
                          />
                          <WalletCardsIcon className="mb-3 h-6 w-6" />
                          Savings Account
                        </Label>
                        <Label
                          htmlFor="MoneyMarketAccount"
                          className={`border cursor-pointer rounded-md p-4 flex flex-col items-center justify-between hover:bg-accent hover:text-accent-foreground [&:has([data-state=checked])]:border-primary`}
                        >
                          <RadioGroupItem
                            {...register('Accounts.0.type', { required: true })}
                            value={AccountType.MoneyMarketAccount}
                            id="MoneyMarketAccount"
                            className="peer sr-only"
                          />
                          <DollarSignIcon className="mb-3 h-6 w-6" />
                          Money Market Account
                        </Label>
                        <Label
                          htmlFor="CertificateOfDeposit"
                          className={`border cursor-pointer rounded-md p-4 flex flex-col items-center justify-between hover:bg-accent hover:text-accent-foreground [&:has([data-state=checked])]:border-primary`}
                        >
                          <RadioGroupItem
                            {...register('Accounts.0.type', { required: true })}
                            value={AccountType.CertificateOfDeposit}
                            id="CertificateOfDeposit"
                            className="peer sr-only"
                          />
                          <DollarSignIcon className="mb-3 h-6 w-6" />
                          Certificate of Deposit
                        </Label>
                      </RadioGroup>
                    )}
                  />
                  {errors.Accounts?.[0]?.type && (
                    <span>{JSON.stringify(errors.Accounts[0].type)}</span>
                  )}
                  <div>
                    <label>Description</label>
                    <input
                      {...register('Accounts.0.description', {
                        required: true,
                      })}
                    />
                    {errors.Accounts?.[0]?.description && (
                      <span>{errors.Accounts?.[0]?.description.message}</span>
                    )}
                  </div>
                  <div>
                    <label>Interest Rate</label>
                    <input
                      type="number"
                      step="0.01"
                      {...register('Accounts.0.interestRate', {
                        required: true,
                      })}
                    />
                    {errors.Accounts?.[0]?.interestRate && (
                      <span>{errors.Accounts?.[0]?.interestRate.message}</span>
                    )}
                  </div>
                  <div>
                    <label>Minimum Balance</label>
                    <input
                      type="number"
                      {...register('Accounts.0.minimumBalance', {
                        required: true,
                      })}
                    />
                    {errors.Accounts?.[0]?.minimumBalance && (
                      <span>
                        {errors.Accounts?.[0]?.minimumBalance.message}
                      </span>
                    )}
                  </div>
                  <div>
                    <label>Allowed Transactions</label>
                    <input
                      type="number"
                      {...register('Accounts.0.allowedTransactions')}
                    />
                    {errors.Accounts?.[0]?.allowedTransactions && (
                      <span>
                        {errors.Accounts?.[0]?.allowedTransactions?.message}
                      </span>
                    )}
                  </div>
                  <div>
                    <label>Early Withdrawal Penalty</label>
                    <input
                      type="checkbox"
                      {...register('Accounts.0.earlyWithdrawalPenalty')}
                    />
                    {errors.Accounts?.[0]?.earlyWithdrawalPenalty && (
                      <span>
                        {errors.Accounts?.[0]?.earlyWithdrawalPenalty?.message}
                      </span>
                    )}
                  </div>
                  <div className="flex justify-end">
                    <Button
                      onClick={() => {
                        setStep(2)
                        console.log(getValues())
                      }}
                    >
                      Next
                    </Button>
                  </div>
                </div>
              )}
              {step === 2 && (
                <div className="grid gap-2">
                  <label
                    htmlFor="email"
                    className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                  >
                    First Name
                  </label>

                  <input
                    className="outline-none bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5  dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                    {...register('FirstName')}
                  />
                  {errors.FirstName?.message && (
                    <p className="mt-2 text-sm text-red-600 dark:text-red-500">
                      {errors.FirstName?.message}
                    </p>
                  )}

                  <label
                    htmlFor="lastname"
                    className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                  >
                    Last Name
                  </label>
                  <input
                    className="outline-none bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5  dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                    {...register('LastName')}
                  />
                  {errors.LastName?.message && (
                    <p className="mt-2 text-sm text-red-600 dark:text-red-500">
                      {errors.LastName?.message}
                    </p>
                  )}

                  <label
                    htmlFor="email"
                    className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                  >
                    Email
                  </label>
                  <input
                    className="outline-none bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5  dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                    {...register('Email')}
                  />
                  {errors.Email?.message && (
                    <p className="mt-2 text-sm text-red-600 dark:text-red-500">
                      {errors.Email?.message}
                    </p>
                  )}

                  <label
                    htmlFor="email"
                    className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                  >
                    Phone
                  </label>
                  <input
                    className="outline-none bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5  dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                    {...register('Phone')}
                  />
                  {errors.Phone?.message && (
                    <p className="mt-2 text-sm text-red-600 dark:text-red-500">
                      {errors.Phone?.message}
                    </p>
                  )}

                  <label
                    htmlFor="email"
                    className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                  >
                    Password
                  </label>
                  <input
                    type="password"
                    className="outline-none bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5  dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                    {...register('Password')}
                  />
                  {errors.Password?.message && (
                    <p className="mt-2 text-sm text-red-600 dark:text-red-500">
                      {errors.Password?.message}
                    </p>
                  )}

                  <div className="flex justify-between">
                    <Button variant="outline" onClick={() => setStep(1)}>
                      Previous
                    </Button>
                    <Button onClick={() => setStep(3)}>Next</Button>
                  </div>
                </div>
              )}
              {step === 3 && (
                <div className="grid gap-6">
                  {/* <div className="space-y-2">
                    <Label htmlFor="documents">Supporting Documents</Label>
                    <Input id="documents" type="file" multiple />
                  </div> */}
                  <div className="flex justify-between">
                    <Button variant="outline" onClick={() => setStep(2)}>
                      Previous
                    </Button>
                    <Button type="submit">Complete Setup</Button>
                  </div>
                </div>
              )}
            </CardContent>
          </Card>
        </>
      )}
    </Form>
  )
}
