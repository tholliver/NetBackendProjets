import { Transaction } from '@/types'
import { CircleCheck, CircleCheckIcon } from 'lucide-react'
import { Card, CardHeader, CardContent, CardFooter } from './ui/card'
import React from 'react'
import { Button } from './ui/button'

interface TransactionState {
  state: string
  transaction: Transaction
}

const TransactionState = (props: TransactionState) => {
  console.log('On the success: ', props.transaction)

  return (
    <div>
      <Card className="bg-background shadow-lg">
        <CardHeader className="flex items-center justify-between">
          <div className="text-2xl font-bold">Transaction Confirmed</div>
          <CircleCheckIcon className="h-8 w-8 text-green-500" />
        </CardHeader>
        <CardContent className="grid gap-6">
          <div className="grid grid-cols-2 gap-4">
            <div>
              <div className="text-muted-foreground">Amount</div>
              <div className="text-2xl font-bold">
                $ {props.transaction.amount}
              </div>
            </div>
            <div>
              <div className="text-muted-foreground">Date</div>
              <div className="text-2xl font-bold">
                {props.transaction.processedDate}
              </div>
            </div>
          </div>
          <div>
            <div className="text-muted-foreground">Recipient Account</div>
            {/* <div className="text-lg font-medium">Paypal Transfer</div> */}
            <div className="text-lg font-medium">
              {props.transaction.targetAccountNumber}
            </div>
          </div>
          <div>
            <div className="text-muted-foreground">Description</div>
            <div>{props.transaction.description}</div>
          </div>
        </CardContent>
        <CardFooter className="flex justify-end">
          <Button>View Transaction</Button>
        </CardFooter>
      </Card>
    </div>
  )
}

export default TransactionState
