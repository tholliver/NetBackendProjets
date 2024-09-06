'use client'
// import Link from 'next/link'
// import {
//   ReceiptIcon,
//   MoveIcon,
//   PowerIcon,
//   ChevronRightIcon,
// } from 'lucide-react'
import {
  Card,
  CardHeader,
  CardTitle,
  CardContent,
  CardDescription,
} from '@/components/ui/card'
import {
  Table,
  TableHeader,
  TableRow,
  TableHead,
  TableCell,
  TableBody,
} from '@/components/ui/table'
import { ComboboxPopover } from '@/components/AccountSelector'
import { useQuery } from '@tanstack/react-query'
import { fetchCustomerAccounts } from '@/services/customers'
import { useState } from 'react'
import { Account } from '@/types'
import QuickActions from './QuickActions'

interface AccountDetailsProps {
  customerFullName: string
  customerId: string
}

export function AccountDetails(props: AccountDetailsProps) {
  const [selectedAccount, setSelectedAccount] = useState<Account | null>(null)
  const token = ''
  const { isPending, error, data } = useQuery({
    queryKey: ['accountData', token, props.customerId],
    queryFn: async () => await fetchCustomerAccounts(token, props.customerId),
  })

  if (isPending) return 'Loading...'

  if (error) return 'An error has occurred: ' + error.message

  return (
    <div>
      <header className="flex mb-2 items-center justify-between bg-background px-6 py-4 shadow">
        <div className="flex items-center gap-4">
          <div className="grid gap-0.5">
            <div className="font-medium">{props.customerFullName}</div>
            {selectedAccount ? (
              <>
                <div className="text-sm text-muted-foreground">
                  {selectedAccount?.type} | Currency:{' '}
                  {selectedAccount?.currency} | Account Balance: ${' '}
                  {selectedAccount?.minimumBalance}
                </div>
              </>
            ) : (
              <p className="text-sm text-muted-foreground">
                Select an account for details
              </p>
            )}
          </div>
        </div>
        <ComboboxPopover
          accounts={data}
          selectedAccount={selectedAccount!}
          setSelectedAccount={setSelectedAccount}
        />
      </header>
      <QuickActions />
      {selectedAccount ? (
        <>
          <Card>
            <CardHeader>
              <CardTitle>Transaction History</CardTitle>
            </CardHeader>
            <CardContent>
              <Table>
                <TableHeader>
                  <TableRow>
                    <TableHead>Date</TableHead>
                    <TableHead>Amount</TableHead>
                    <TableHead>Description</TableHead>
                  </TableRow>
                </TableHeader>
                {selectedAccount?.transactions?.length! > 0 ? (
                  <TableBody>
                    {selectedAccount?.transactions?.map((transaction) => (
                      <TableRow key={transaction.transactionId}>
                        <TableCell>{transaction.processedDate}</TableCell>
                        <TableCell>$ {transaction.amount}</TableCell>
                        <TableCell>{transaction.description}</TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                ) : (
                  <TableBody>
                    <TableRow>
                      <TableCell colSpan={3} className="text-center">
                        No transactions
                      </TableCell>
                    </TableRow>
                  </TableBody>
                )}
              </Table>
            </CardContent>
          </Card>
        </>
      ) : (
        <>
          <>
            <h2 className="text-lg font-medium">Accounts</h2>
            <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
              {data.map((account) => (
                <Card key={account.accountId}>
                  <CardHeader>
                    <CardTitle>{account.type}</CardTitle>
                    <CardDescription>
                      Account #: {account.accountNumber}
                    </CardDescription>
                  </CardHeader>
                  <CardContent>
                    <div className="flex items-center justify-between">
                      <div className="text-sm text-muted-foreground">
                        Balance
                      </div>
                      <div className="text-lg font-medium">
                        ${account.minimumBalance}
                      </div>
                    </div>
                  </CardContent>
                </Card>
              ))}
            </div>
            <div className="py-4">
              <Card>
                <h2 className="text-xl font-bold m-4">Recent Transactions</h2>
                <Table>
                  <TableHeader>
                    <TableRow>
                      <TableHead>Date</TableHead>
                      <TableHead>Amount</TableHead>
                      <TableHead>Description</TableHead>
                    </TableRow>
                  </TableHeader>
                  <TableBody>
                    <TableRow>
                      <TableCell>2023-04-15</TableCell>
                      <TableCell>$50.00</TableCell>
                      <TableCell>Grocery Store</TableCell>
                    </TableRow>
                    <TableRow>
                      <TableCell>2023-04-12</TableCell>
                      <TableCell>$25.00</TableCell>
                      <TableCell>Coffee Shop</TableCell>
                    </TableRow>
                    <TableRow>
                      <TableCell>2023-04-10</TableCell>
                      <TableCell>$100.00</TableCell>
                      <TableCell>Rent Payment</TableCell>
                    </TableRow>
                    <TableRow>
                      <TableCell>2023-04-05</TableCell>
                      <TableCell>$75.00</TableCell>
                      <TableCell>Utility Bill</TableCell>
                    </TableRow>
                  </TableBody>
                </Table>
              </Card>
            </div>
          </>
        </>
      )}
    </div>
  )
}

export default AccountDetails
