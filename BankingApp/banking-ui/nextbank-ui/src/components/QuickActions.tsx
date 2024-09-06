import React from 'react'
import { Card, CardHeader, CardContent } from './ui/card'
import { ExpandIcon, MoveIcon, CheckIcon, DeleteIcon } from 'lucide-react'
import { Button } from './ui/button'
import { BellIcon } from '@radix-ui/react-icons'
import Link from 'next/link'

const QuickActions = () => {
  return (
    <div>
      <Card>
        <CardHeader className="flex flex-row">
          <div className="text-md font-medium">Quick Actions</div>
          {/* <ExpandIcon className="h-2 w-2 text-muted-foreground" /> */}
        </CardHeader>
        <CardContent className="grid gap-4 grid-cols-2 md:grid-cols-4 lg:grid-cols-4 ">
          <Link
            className="inline-flex items-center whitespace-nowrap text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 border border-input bg-background hover:bg-accent hover:text-accent-foreground h-9 rounded-md px-3 justify-start gap-2"
            href="/customer/transaction"
          >
            <MoveIcon className="h-4 w-4" />
            Transfer
          </Link>
          <Link
            href="#"
            className="inline-flex items-center whitespace-nowrap text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 border border-input bg-background hover:bg-accent hover:text-accent-foreground h-9 rounded-md px-3 justify-start gap-2"
          >
            <BellIcon className="h-4 w-4" />
            Pay Bills
          </Link>
          <Link
            href="#"
            className="inline-flex items-center whitespace-nowrap text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 border border-input bg-background hover:bg-accent hover:text-accent-foreground h-9 rounded-md px-3 justify-start gap-2"
          >
            <CheckIcon className="h-4 w-4" />
            Deposit
          </Link>
          <Link
            href="#"
            className="inline-flex items-center whitespace-nowrap text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 border border-input bg-background hover:bg-accent hover:text-accent-foreground h-9 rounded-md px-3 justify-start gap-2"
          >
            <DeleteIcon className="h-4 w-4" />
            Withdraw
          </Link>
        </CardContent>
      </Card>
    </div>
  )
}

export default QuickActions
