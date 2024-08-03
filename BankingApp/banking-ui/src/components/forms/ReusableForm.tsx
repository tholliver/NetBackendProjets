'use client'
import { useMutation } from '@tanstack/react-query'
import { ChangeEvent, FormEvent, useState } from 'react'
import axios, { type AxiosError } from 'axios'
import { LucideAlertTriangle } from 'lucide-react'
import { zodResolver } from '@hookform/resolvers/zod'
import { FieldValues, useForm } from 'react-hook-form'
import { SignUpSchemaType, customerSchema } from '@/schemas'
import { ZodType } from 'zod'

export interface rFormData {
  [key: string]: string
}
declare module '@tanstack/react-query' {
  interface Register {
    defaultError: AxiosError
  }
}
interface ReusableFormProps {
  uriToPost: string
  inputProps: {
    [key: string]: {
      type?: string
      placeholder: string
    }
  }
}

export interface IZodError {
  code: string
  validation: string
  message: string
  path: string[]
}

export function ReusableForm({ uriToPost, inputProps }: ReusableFormProps) {
  const [formData, setFormData] = useState<rFormData>({})
  // const {
  //   register,
  //   handleSubmit,
  //   formState: { errors },
  // } = useForm({ resolver: zodResolver() })

  const mutation = useMutation<void, AxiosError<IZodError[], any>, rFormData>({
    mutationFn: (formData: rFormData) => {
      return axios.post(`${uriToPost}`, formData)
    },
  })

  // For json types
  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    mutation.mutate(formData)
    console.log('Errors: ', mutation.error)
    console.log('Data: ', mutation.data)
  }

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value })
  }

  return (
    <form className="max-w-md mx-auto" onSubmit={handleSubmit}>
      {Object.entries(inputProps).map(([name, props]) => (
        <div key={name} className="relative z-0 w-full mb-5 group">
          <input
            id={name}
            type={props.type || 'text'}
            name={name}
            className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
            value={formData[name] || ''}
            onChange={handleChange}
            placeholder=""
            // {...register(name)}
          />
          <label
            className="peer-focus:font-medium absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
            htmlFor={name}
          >
            {props.placeholder}
          </label>
          {/* {errors[key as keyof T] && <span>{errors.root?.message}</span>} */}
        </div>
      ))}

      {/* <div className="px-3 py-3 text-xs font-medium leading-none text-center text-blue-800 bg-blue-200 rounded-md animate-pulse dark:bg-blue-900 dark:text-blue-200">
        <p>Adding data...</p>
      </div> */}
      {mutation.isPending ? (
        <div className="px-3 py-3 text-xs font-medium leading-none text-center text-blue-800 bg-blue-200 rounded-md animate-pulse dark:bg-blue-900 dark:text-blue-200">
          <p>Adding data...</p>
        </div>
      ) : (
        <button
          type="submit"
          disabled={mutation.isPending}
          className="text-white w-full bg-blue-700 hover:bg-blue-800 focus:ring-2 focus:outline-none focus:ring-blue-300 rounded-md font-medium text-sm px-5 py-2 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800 "
        >
          Submit
        </button>
      )}

      <>
        {mutation.error?.isAxiosError ? (
          <div
            id="alert-5"
            className="flex items-center justify-center p-3 rounded-lg bg-gray-50 dark:bg-gray-800"
            role="alert"
          >
            <LucideAlertTriangle className="text-red-500" />

            <span className="sr-only">Info</span>
            <div className="ms-3  text-sm font-medium text-gray-800 dark:text-red-500">
              Error:{' '}
              {JSON.stringify(mutation.error.response?.data.at(0)?.message)}
            </div>
          </div>
        ) : null}
        {mutation.isSuccess ? (
          <div
            id="alert-3"
            className="flex items-center justify-center p-4 mb-4 text-green-800 rounded-lg bg-green-50 dark:bg-gray-800 dark:text-green-400"
            role="alert"
          >
            <LucideAlertTriangle />
            <div className="ms-3 text-sm font-medium">Teacher Added with</div>
            <button
              type="button"
              className="ms-auto -mx-1.5 -my-1.5 bg-green-50 text-green-500 rounded-lg focus:ring-2 focus:ring-green-400 p-1.5 hover:bg-green-200 inline-flex items-center justify-center h-8 w-8 dark:bg-gray-800 dark:text-green-400 dark:hover:bg-gray-700"
              data-dismiss-target="#alert-3"
              aria-label="Close"
            ></button>
          </div>
        ) : null}
      </>
    </form>
  )
}

// export default ReusableForm
