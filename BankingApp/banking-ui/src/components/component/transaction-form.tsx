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

export function TransactionForm() {
  return (
    <Card className="w-full max-w-md mx-auto">
      <CardHeader>
        <CardTitle>Transfer Funds</CardTitle>
        <CardDescription>
          Complete the form to transfer money to another account.
        </CardDescription>
      </CardHeader>
      <CardContent className="space-y-4">
        <div className="space-y-2">
          <Label htmlFor="recipient-name">Recipient Name</Label>
          <Input id="recipient-name" placeholder="Enter recipient's name" />
        </div>
        <div className="space-y-2">
          <Label htmlFor="account-number">Account Number</Label>
          <Input
            id="account-number"
            type="number"
            placeholder="Enter account number"
          />
        </div>
        <div className="space-y-2">
          <Label htmlFor="amount">Amount</Label>
          <Input
            id="amount"
            type="number"
            placeholder="Enter amount to transfer"
          />
        </div>
        <div className="space-y-2">
          <Label htmlFor="note">Note (optional)</Label>
          <Textarea id="note" placeholder="Add a note for the transfer" />
        </div>
      </CardContent>
      <CardFooter>
        <div className="flex items-center justify-between">
          <Button variant="outline">Review Transfer</Button>
          <Button type="submit">Transfer Funds</Button>
        </div>
      </CardFooter>
    </Card>
  )
}
