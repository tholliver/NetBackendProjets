'use client'
import {
  Card,
  CardHeader,
  CardTitle,
  CardDescription,
  CardContent,
  CardFooter,
} from '@/components/ui/card'
import { Label } from '@/components/ui/label'
import { Input } from '@/components/ui/input'
import { Textarea } from '@/components/ui/textarea'
import { Button } from '@/components/ui/button'
import { useMutation, useQuery } from '@tanstack/react-query'
import { fetchCustomerAccounts, pullTransaction } from '@/services/customers'
import { useForm } from 'react-hook-form'
import { Transaction } from '@/types'
import { TransactionV, transactionSchema } from '@/schemas'
import { zodResolver } from '@hookform/resolvers/zod'
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from '../ui/form'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { ExclamationTriangleIcon } from '@radix-ui/react-icons'

import { Alert, AlertDescription, AlertTitle } from '@/components/ui/alert'
import TransactionState from '../TransactionState'

interface TransactionFormProps {
  customerId: string
}

export function TransactionForm(props: TransactionFormProps) {
  const token = 'j'

  const { isPending, error, data } = useQuery({
    queryKey: ['accountData', token, props.customerId],
    queryFn: async () => await fetchCustomerAccounts(token, props.customerId),
  })

  const mutation = useMutation({
    mutationFn: async (transaction: Transaction) => {
      return await pullTransaction(transaction)
    },
  })

  const form = useForm<TransactionV>({
    resolver: zodResolver(transactionSchema),
  })

  const {
    register,
    handleSubmit,
    setValue,
    formState: { errors },
  } = form
  // getValues,
  // watch,

  function onSubmit(values: any) {
    console.log(values)
    mutation.mutate(values)
    console.log('Here the error: ', mutation)
  }

  function searchAccountId(accountNumber: string) {
    return data?.find((account) => account.accountNumber === accountNumber)
      ?.accountId
  }

  const transaction: Transaction = {
    transactionId: 1,
    sourceAccountNumber: '12345678',
    targetAccountNumber: '87654321',
    status: 1,
    amount: 1000,
    createdDate: '2024-09-05T10:00:00',
    processedDate: '2024-09-06T12:00:00',
    description: 'Pago de factura #123',
    transactionType: 1,
    accountId: 1001,
  }

  if (isPending) return 'Loading...'
  if (error) return 'An error has occurred: ' + error.message
  return (
    <Card className="w-full max-w-md mx-auto">
      {!mutation.isSuccess ? (
        <>
          <TransactionState
            // transaction={mutation.data}
            transaction={transaction}
            state="Successed transaction"
          />
        </>
      ) : (
        <>
          <Form {...form}>
            <form onSubmit={handleSubmit(onSubmit)}>
              <CardHeader>
                <CardTitle>Transfer Funds</CardTitle>
                <CardDescription>
                  Complete the form to transfer money to another account.
                </CardDescription>
              </CardHeader>
              {mutation.isSuccess ? <div>Todo added!</div> : null}
              <CardContent className="space-y-4">
                {mutation.isError && (
                  <>
                    <Alert variant="destructive">
                      <ExclamationTriangleIcon className="h-4 w-4" />
                      <AlertTitle>Error</AlertTitle>
                      <AlertDescription>
                        Error somethig happened. Please try again. Posible
                        Solutions:
                        <ul>
                          <li>- Check have enought founds.</li>
                          <li>- Verify the Recipent Number</li>
                        </ul>
                      </AlertDescription>
                    </Alert>
                  </>
                )}
                <div className="space-y-2">
                  <FormField
                    control={form.control}
                    name="SourceAccountNumber"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Account number</FormLabel>
                        <Select
                          onValueChange={(value) => {
                            field.onChange(value)
                            setValue('AccountId', searchAccountId(value)!)
                            setValue('Status', 1)
                            setValue('CreatedDate', new Date())
                            setValue('ProcessedDate', new Date())
                            setValue('TransactionType', 1)
                          }}
                          defaultValue={field.value}
                        >
                          <FormControl>
                            <SelectTrigger>
                              <SelectValue placeholder="Select a account to transfer from" />
                            </SelectTrigger>
                          </FormControl>
                          <SelectContent>
                            {data.map((account) => (
                              <SelectItem
                                key={account.accountId}
                                value={account.accountNumber}
                              >
                                {account.accountNumber}
                              </SelectItem>
                            ))}
                          </SelectContent>
                        </Select>
                        {/* <FormDescription>
                      You can manage email addresses in your{' '}
                      <Link href="/examples/forms">email settings</Link>.
                    </FormDescription> */}
                        <FormMessage />
                      </FormItem>
                    )}
                  />
                </div>

                <div className="space-y-2">
                  <Label htmlFor="recipient-name">Recipient Name</Label>
                  <Input
                    id="recipient-name"
                    placeholder="Enter recipient's name"
                  />
                </div>
                <div className="space-y-2">
                  <Label htmlFor="account-number">Account Number</Label>
                  <Input
                    id="account-number"
                    type="number"
                    {...register('TargetAccountNumber')}
                    placeholder="Enter account number"
                  />
                  {errors.TargetAccountNumber?.message && (
                    <p className="mt-2 text-sm text-red-600 dark:text-red-500">
                      {errors.TargetAccountNumber?.message}
                    </p>
                  )}
                </div>
                <div className="space-y-2">
                  <Label htmlFor="amount">Amount</Label>
                  <Input
                    id="amount"
                    {...register('Amount')}
                    type="number"
                    placeholder="Enter amount to transfer"
                  />
                  {errors.Amount?.message && (
                    <p className="mt-2 text-sm text-red-600 dark:text-red-500">
                      {errors.Amount?.message}
                    </p>
                  )}
                </div>
                <div className="space-y-2">
                  <Label htmlFor="note">Note (optional)</Label>
                  <Textarea
                    {...register('Description')}
                    id="note"
                    placeholder="Add a note for the transfer"
                  />
                </div>
                {errors.Description?.message && (
                  <p className="mt-2 text-sm text-red-600 dark:text-red-500">
                    {errors.Description?.message}
                  </p>
                )}
              </CardContent>
              <CardFooter>
                <div className="flex items-center justify-between">
                  <Button
                    onClick={() => {
                      console.log('', errors)
                    }}
                    variant="outline"
                  >
                    Review
                  </Button>
                  <Button disabled={mutation.isPending} type="submit">
                    Transfer Funds
                  </Button>
                </div>
              </CardFooter>
            </form>
          </Form>
        </>
      )}
    </Card>
  )
}
