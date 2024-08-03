import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group'
import { Label } from '@/components/ui/label'
import { CreditCardIcon, DollarSignIcon, WalletCardsIcon } from '../icons'

export function Election() {
  return (
    <RadioGroup
      defaultValue="CheckingAccount"
      className="grid grid-cols-3 gap-4"
    >
      <Label
        htmlFor="CheckingAccount"
        className="border cursor-pointer rounded-md p-4 flex flex-col items-center justify-between hover:bg-accent hover:text-accent-foreground [&:has([data-state=checked])]:border-primary"
      >
        <RadioGroupItem
          value="CheckingAccount"
          id="CheckingAccount"
          className="peer sr-only"
        />
        <CreditCardIcon className="mb-3 h-6 w-6" />
        Checking Account
      </Label>
      <Label
        htmlFor="SavingsAccount"
        className="border cursor-pointer rounded-md p-4 flex flex-col items-center justify-between hover:bg-accent hover:text-accent-foreground [&:has([data-state=checked])]:border-primary"
      >
        <RadioGroupItem
          value="SavingsAccount"
          id="SavingsAccount"
          className="peer sr-only"
        />
        <WalletCardsIcon className="mb-3 h-6 w-6" />
        Savings Account
      </Label>
      <Label
        htmlFor="MoneyMarketAccount"
        className="border cursor-pointer rounded-md p-4 flex flex-col items-center justify-between hover:bg-accent hover:text-accent-foreground [&:has([data-state=checked])]:border-primary"
      >
        <RadioGroupItem
          value="MoneyMarketAccount"
          id="MoneyMarketAccount"
          className="peer sr-only"
        />
        <DollarSignIcon className="mb-3 h-6 w-6" />
        Money Market Account
      </Label>
      <Label
        htmlFor="CertificateOfDeposit"
        className="border cursor-pointer rounded-md p-4 flex flex-col items-center justify-between hover:bg-accent hover:text-accent-foreground [&:has([data-state=checked])]:border-primary"
      >
        <RadioGroupItem
          value="CertificateOfDeposit"
          id="CertificateOfDeposit"
          className="peer sr-only"
        />
        <DollarSignIcon className="mb-3 h-6 w-6" />
        Certificate of Deposit
      </Label>
    </RadioGroup>
  )
}
