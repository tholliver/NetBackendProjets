'use client'
import React, { FormEvent } from 'react'
import {
  useForm,
  type FieldValues,
  type SubmitHandler,
  type UseFormReturn,
} from 'react-hook-form'

import { CustomerTypo, AccountType, Account } from '@/models'
import { customerSchema } from '@/schemas'
import { useMutation } from '@tanstack/react-query'
import axios, { AxiosError } from 'axios'
import { IZodError } from '@/types'
import { ZodType } from 'zod'
import { zodResolver } from '@hookform/resolvers/zod'

type FormProps<TFormValues extends FieldValues> = {
  onSubmit: SubmitHandler<TFormValues>
  children: (methods: UseFormReturn<TFormValues>) => React.ReactNode
  schema: ZodType<TFormValues>
}

// const Form =

export const Form = <TFormValues extends FieldValues>({
  onSubmit,
  children,
  schema,
}: FormProps<TFormValues>) => {
  const methods = useForm<TFormValues>({
    resolver: zodResolver(schema),
  })
  return (
    <form
      className="max-w-sm mx-auto"
      onSubmit={methods.handleSubmit(onSubmit)}
    >
      {children(methods)}
    </form>
  )
}

export default function Formix() {
  const mutation = useMutation<
    void,
    AxiosError<IZodError[], any>,
    CustomerTypo
  >({
    mutationFn: (formData: CustomerTypo) => {
      return axios.post('/', formData)
    },
  })

  const onSubmit = (data: CustomerTypo) => {
    // event.preventDefault()
    mutation.mutate(data)
    console.log('Errors: ', mutation.error)
    console.log('Data: ', mutation.data)
  }

  return (
    <div className="grid place-items-center justify-center ">
      <h2 className="text-4xl mb-2 font-extrabold dark:text-white">
        Sign Up Form
      </h2>
      <Form<CustomerTypo> onSubmit={onSubmit} schema={customerSchema}>
        {({ register, formState: { errors } }) => (
          <>
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

            <button type="submit">Sign UP</button>
          </>
        )}
      </Form>
    </div>
  )
}
