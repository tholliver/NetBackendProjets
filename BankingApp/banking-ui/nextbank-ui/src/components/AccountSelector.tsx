'use client'

import * as React from 'react'
import { LucideIcon, Banknote } from 'lucide-react'

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
import { Account } from '@/types'
import { auth } from '@/auth'
import Link from 'next/link'

interface ComboboxPopoverProps {
  accounts?: Account[]
  selectedAccount: Account
  setSelectedAccount: React.Dispatch<React.SetStateAction<Account | null>>
}

export function ComboboxPopover(props: ComboboxPopoverProps) {
  const [open, setOpen] = React.useState(false)
  // const [selectedAccount, setSelectedAccount] = React.useState<Account | null>(
  //   null
  // )

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
            {props.selectedAccount ? (
              <>
                <Banknote className="mr-2 h-4 w-4 shrink-0" />
                {props.selectedAccount.accountNumber}
              </>
            ) : (
              <>+ S. Account</>
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
                  <CommandItem
                    key={account.accountId}
                    value={account.accountNumber}
                    onSelect={(value) => {
                      props.setSelectedAccount(
                        props.accounts?.find(
                          (account) => account.accountNumber === value
                        ) || null
                      )
                      setOpen(false)
                    }}
                  >
                    <Banknote
                      className={cn(
                        'mr-2 h-4 w-4',
                        account.accountNumber ===
                          props.selectedAccount?.accountNumber
                          ? 'opacity-100'
                          : 'opacity-40'
                      )}
                    />
                    <span>{account.accountNumber}</span>
                  </CommandItem>
                ))}
              </CommandGroup>
            </CommandList>
          </Command>
        </PopoverContent>
      </Popover>
    </div>
  )
}
