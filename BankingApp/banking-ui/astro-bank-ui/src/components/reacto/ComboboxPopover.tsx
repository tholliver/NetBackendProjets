import * as React from 'react'
// import { LucideIcon, Banknote } from 'lucide-react'
import { BankNote } from '../icons/Icons'

import { cn } from '@/lib/utils'
import { Button } from '@/components/ui/button'
import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
  CommandList,
} from '@/components/ui/command'
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from '@/components/ui/popover'
import type { Account } from '@/types'

interface ComboboxPopoverProps {
  accounts?: Account[]
  // handleAccountChange: () => void
}

export function ComboboxPopover(props: ComboboxPopoverProps) {
  const [open, setOpen] = React.useState(false)
  const [selectedAccount, setSelectedAccount] = React.useState<Account | null>(
    null
  )

  return (
    <div className="flex items-center space-x-4">
      <p className="text-sm text-muted-foreground">Accounts</p>
      <Popover open={open} onOpenChange={setOpen}>
        <PopoverTrigger asChild>
          <Button
            variant="outline"
            size="sm"
            className="w-[150px] justify-start"
          >
            {selectedAccount ? (
              <>
                <BankNote className="mr-2 h-4 w-4 shrink-0" />
                {selectedAccount.accountNumber}
              </>
            ) : (
              <>+ Set status</>
            )}
          </Button>
        </PopoverTrigger>
        <PopoverContent className="p-0" side="right" align="start">
          <Command>
            <CommandInput placeholder="Account number..." />
            <CommandList>
              <CommandEmpty>No results found.</CommandEmpty>
              <CommandGroup>
                {props.accounts?.map((account) => (
                  <a
                    key={account.accountId}
                    href={`customer/${account.accountId}`}
                  >
                    <CommandItem
                      value={account.accountNumber}
                      onSelect={(value) => {
                        console.log('Here on the Select value: ', value)

                        setSelectedAccount(
                          props.accounts?.find(
                            (priority) => priority.accountNumber === value
                          ) || null
                        )
                        setOpen(false)
                      }}
                    >
                      <BankNote
                        className={cn(
                          'mr-2 h-4 w-4',
                          account.accountNumber ===
                            selectedAccount?.accountNumber
                            ? 'opacity-100'
                            : 'opacity-40'
                        )}
                      />
                      <span>{account.accountNumber}</span>
                    </CommandItem>
                  </a>
                ))}
              </CommandGroup>
            </CommandList>
          </Command>
        </PopoverContent>
      </Popover>
    </div>
  )
}
