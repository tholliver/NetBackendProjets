'use client'

import { useState } from 'react'
import {
  Card,
  CardHeader,
  CardTitle,
  CardDescription,
  CardContent,
} from '@/components/ui/card'

import { Button } from '@/components/ui/button'
import { AccountType, CustomerType } from '@/models'
import { customerSchema, SignUpSchemaType } from '@/schemas'

import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from '../ui/form'
import { RadioGroup, RadioGroupItem } from '../ui/radio-group'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { CreditCardIcon, DollarSignIcon, WalletCardsIcon } from '../icons'
import { Label } from '../ui/label'
import { Input } from '../ui/input'
import { Checkbox } from '../ui/checkbox'
import Link from 'next/link'

export function SignUpForm() {
  const [step, setStep] = useState(1)
  const form = useForm<SignUpSchemaType>({
    resolver: zodResolver(customerSchema),
  })

  const {
    register,
    handleSubmit,
    trigger,
    getValues,
    watch,
    formState: { errors },
  } = form

  const onSubmit = (data: CustomerType) => {
    console.log('Data submited', data)
  }

  return (
    <Form {...form}>
      <form onSubmit={handleSubmit(onSubmit)}>
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
                <FormField
                  control={form.control}
                  name="Accounts.0.type"
                  render={({ field }) => (
                    <FormItem className="space-y-3">
                      <FormLabel>Choose a account type...</FormLabel>
                      <FormControl>
                        <RadioGroup
                          className="grid grid-cols-3 gap-4"
                          onValueChange={field.onChange}
                          defaultValue={field.name}
                        >
                          <Label
                            htmlFor="CheckingAccount"
                            className={`border cursor-pointer rounded-md p-4 flex flex-col items-center justify-between hover:bg-accent hover:text-accent-foreground [&:has([data-state=checked])]:border-primary`}
                          >
                            <RadioGroupItem
                              checked={
                                getValues('Accounts.0.type') ===
                                AccountType.CheckingAccount
                              }
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
                              checked={
                                getValues('Accounts.0.type') ===
                                AccountType.SavingsAccount
                              }
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
                              checked={
                                getValues('Accounts.0.type') ===
                                AccountType.MoneyMarketAccount
                              }
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
                              checked={
                                getValues('Accounts.0.type') ===
                                AccountType.CertificateOfDeposit
                              }
                              value={AccountType.CertificateOfDeposit}
                              id="CertificateOfDeposit"
                              className="peer sr-only"
                            />
                            <DollarSignIcon className="mb-3 h-6 w-6" />
                            Certificate of Deposit
                          </Label>
                        </RadioGroup>
                      </FormControl>
                      <FormMessage className="" />
                    </FormItem>
                  )}
                />

                <div>
                  <FormItem>
                    <FormLabel>Currency</FormLabel>
                    <Input {...register('Accounts.0.currency')} />
                    {/* {errors.Accounts?.[0]?.description && (
                    <span>{errors.Accounts?.[0]?.description.message}</span>
                  )} */}
                    <FormMessage />
                  </FormItem>
                </div>
                <div>
                  <FormItem>
                    <FormLabel>Description</FormLabel>
                    <Input {...register('Accounts.0.description')} />
                    {/* {errors.Accounts?.[0]?.description && (
                    <span>{errors.Accounts?.[0]?.description.message}</span>
                  )} */}
                    <FormMessage />
                  </FormItem>
                </div>
                <div>
                  <FormLabel>Interest Rate</FormLabel>
                  <Input {...register('Accounts.0.interestRate')} />
                  {/* {errors.Accounts?.[0]?.interestRate && (
                    <span>{errors.Accounts?.[0]?.interestRate.message}</span>
                  )} */}
                </div>
                <div>
                  <FormLabel>Minimum Balance</FormLabel>
                  <Input {...register('Accounts.0.minimumBalance')} />
                  {errors.Accounts?.[0]?.minimumBalance && (
                    <span>{errors.Accounts?.[0]?.minimumBalance.message}</span>
                  )}
                </div>
                <div>
                  <FormLabel>Allowed Transactions</FormLabel>
                  <Input {...register('Accounts.0.allowedTransactions')} />
                  {errors.Accounts?.[0]?.allowedTransactions && (
                    <span>
                      {errors.Accounts?.[0]?.allowedTransactions?.message}
                    </span>
                  )}
                </div>
                <div>
                  <FormLabel>Early Withdrawal Penalty</FormLabel>

                  <FormField
                    control={form.control}
                    name="Accounts.0.earlyWithdrawalPenalty"
                    render={({ field }) => (
                      <FormItem className="flex flex-row items-start space-x-3 space-y-0 rounded-md border p-4">
                        <FormControl>
                          <Checkbox
                            checked={field.value}
                            onCheckedChange={field.onChange}
                          />
                        </FormControl>
                        <div className="space-y-1 leading-none">
                          <FormLabel>Terms and conditions</FormLabel>
                          <FormDescription>
                            You can manage your mobile notifications in the{' '}
                            <Link href="/examples/forms">mobile settings</Link>{' '}
                            page.
                          </FormDescription>
                        </div>
                      </FormItem>
                    )}
                  />
                  {errors.Accounts?.[0]?.earlyWithdrawalPenalty && (
                    <span>
                      {errors.Accounts?.[0]?.earlyWithdrawalPenalty?.message}
                    </span>
                  )}
                </div>
                <div className="flex justify-end">
                  <Button
                    onClick={async () => {
                      const valid = await trigger([
                        'Accounts.0.type',
                        'Accounts.0.description',
                        'Accounts.0.interestRate',
                        'Accounts.0.minimumBalance',
                        'Accounts.0.allowedTransactions',
                        'Accounts.0.earlyWithdrawalPenalty',
                      ])
                      console.log(form.getValues())

                      if (valid) {
                        setStep(2)
                      }
                    }}
                  >
                    Next
                  </Button>
                </div>
              </div>
            )}
            {step === 2 && (
              <div className="grid gap-2">
                <section>
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
                </section>
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
      </form>
    </Form>
  )
}
