'use client'
import { zodResolver } from '@hookform/resolvers/zod'
import { useRouter } from 'next/navigation'
import { useForm } from 'react-hook-form'
import { authenticate } from '@/lib/actions'
import { loginSchema, User } from '@/schemas'
import { ICredentials } from '@/types'
import { useState } from 'react'
import { Card, CardContent, CardFooter } from '@/components/ui/card'
import { Label } from '@/components/ui/label'
import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'
import Link from 'next/link'
export default function LoginForm() {
  const router = useRouter()
  const [authFnErrors, setAuthFnErrors] = useState<string | undefined>()

  const {
    handleSubmit,
    register,
    formState: { errors, isSubmitting, isDirty, isValid },
  } = useForm<User>({
    resolver: zodResolver(loginSchema),
  })

  async function onSubmit(data: ICredentials) {
    const authMessage = await authenticate(data)
    setAuthFnErrors(authMessage)

    if (!authMessage) {
      router.push('/tweets')
    }
  }

  return (
    <div className="flex items-center h-dvh">
      <div className="mx-auto max-w-md space-y-6">
        {/* Form Body */}
        <div className="space-y-2 text-center">
          <h1 className="text-3xl font-bold">Login</h1>
          <p className="text-muted-foreground">
            Enter your email and password to access your account.
          </p>
        </div>
        <Card>
          <form className="mt-8 space-y-6" onSubmit={handleSubmit(onSubmit)}>
            <CardContent className="space-y-4">
              <div className="space-y-2">
                <Label
                  htmlFor="email"
                  className="left-0 -top-3.5 text-sm text-gray-600 transition-all peer-placeholder-shown:top-2 peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-400 peer-focus:-top-3.5 peer-focus:text-sm peer-focus:text-gray-600"
                >
                  Email address
                </Label>
                <Input
                  {...register('email', { required: true })}
                  id="email"
                  name="email"
                  type="text"
                  className={`peer h-10 w-full border-b-2 text-gray-900 placeholder-transparent focus:outline-none ${
                    errors.email ? 'border-red-600' : 'border-gray-300'
                  } focus:border-gray-600`}
                  placeholder="john@doe.com"
                  autoComplete="off"
                />
                {errors?.email && (
                  <p className="text-red-600 text-sm">
                    {errors?.email?.message}
                  </p>
                )}
              </div>

              {/* Password Input */}
              <div className=" space-y-2">
                <Label
                  htmlFor="password"
                  className="left-0 -top-3.5 text-sm text-gray-600 transition-all peer-placeholder-shown:top-2 peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-400 peer-focus:-top-3.5 peer-focus:text-sm peer-focus:text-gray-600"
                >
                  Password
                </Label>
                <Input
                  {...register('password', { required: true })}
                  id="password"
                  type="password"
                  name="password"
                  className={`peer h-10 w-full border-b-2 text-gray-900 placeholder-transparent focus:outline-none ${
                    errors.password ? 'border-red-600' : 'border-gray-300'
                  } focus:border-gray-600`}
                  placeholder="Password"
                  autoComplete="off"
                />
                {errors?.password && (
                  <p className="text-red-600 text-sm">
                    {errors?.password?.message}
                  </p>
                )}
              </div>
            </CardContent>

            {/* Server Errors */}
            {authFnErrors && (
              <p className="mt-2 text-center text-red-600 text-sm">
                {authFnErrors}
              </p>
            )}

            {/* Submit Button */}
            {/* Forgot Password Link */}
            <CardFooter className="flex flex-col items-center gap-2">
              <Button
                type="submit"
                disabled={!isDirty || !isValid || isSubmitting}
                className={`w-full  ${
                  isSubmitting
                    ? 'cursor-wait opacity-70'
                    : 'focus:ring focus:ring-gray-500 focus:ring-opacity-80 focus:ring-offset-2'
                }`}
              >
                {isSubmitting ? (
                  <div role="status">Signing In...</div>
                ) : (
                  'Sign In'
                )}
              </Button>
              {/* <Button type="submit" className="w-full">
              Sign in
            </Button> */}
              <Link
                href="#"
                className="text-sm text-muted-foreground underline underline-offset-4 hover:text-primary-foreground"
                prefetch={false}
              >
                Forgot password?
              </Link>
            </CardFooter>
          </form>
        </Card>
      </div>
    </div>
  )
}
