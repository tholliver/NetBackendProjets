'use client'
import { FieldValues, Path, useForm, DefaultValues } from 'react-hook-form'
import { z, ZodType } from 'zod'
import { zodResolver } from '@hookform/resolvers/zod'
import { ChangeEvent, FormEvent } from 'react'

import { useMutation } from '@tanstack/react-query'
import axios, { AxiosError } from 'axios'

interface InputProps {
  [key: string]: {
    type?: string
    placeholder: string
  }
}

interface ReusableFormProps<T extends FieldValues> {
  uriToPost: string
  inputProps: InputProps
  schema: ZodType<T>
}

const MadeForm = <T extends FieldValues>({
  uriToPost,
  inputProps,
  schema,
}: ReusableFormProps<T>) => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<T>({
    resolver: zodResolver(schema),
  })

  const mutation = useMutation<void, AxiosError, T>({
    mutationFn: (formData: T) => {
      return axios.post(uriToPost, formData)
    },
  })

  const handleMutation = (event: FormEvent<HTMLFormElement>, data: T) => {
    event.preventDefault()
    mutation.mutate(data)
    console.log('Errors: ', mutation.error)
    console.log('Data: ', mutation.data)
  }

  return (
    <form
      className="max-w-md mx-auto"
      onSubmit={handleSubmit((data: T) => {
        mutation.mutate(data)
      })}
    >
      {Object.entries(inputProps).map(([name, props]) => (
        <div key={name} className="relative z-0 w-full mb-5 group">
          <input
            id={name}
            type={props.type || 'text'}
            className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
            placeholder=""
            {...register(name as Path<T>)}
          />
          <label
            className="peer-focus:font-medium absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
            htmlFor={name}
          >
            {props.placeholder}
          </label>
          {errors[name as keyof T] && (
            <span>
              {(errors[name as keyof T]?.message as string) ||
                `${name} is required`}
            </span>
          )}
        </div>
      ))}

      {mutation.isPending ? (
        <div className="px-3 py-3 text-xs font-medium leading-none text-center text-blue-800 bg-blue-200 rounded-md animate-pulse dark:bg-blue-900 dark:text-blue-200">
          <p>Adding data...</p>
        </div>
      ) : (
        <button
          type="submit"
          disabled={mutation.isPending}
          className="text-white w-full bg-blue-700 hover:bg-blue-800 focus:ring-2 focus:outline-none focus:ring-blue-300 rounded-md font-medium text-sm px-5 py-2 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
        >
          Submit
        </button>
      )}

      {mutation.isError && mutation.error.isAxiosError && (
        <div
          id="alert-5"
          className="flex items-center justify-center p-3 rounded-lg bg-gray-50 dark:bg-gray-800"
          role="alert"
        >
          <span className="text-red-500">Error: </span>
          <span className="ms-3 text-sm font-medium text-gray-800 dark:text-red-500">
            {/* {JSON.stringify(mutation.error.response?.data.at(0)?.message)} */}
          </span>
        </div>
      )}
      {mutation.isSuccess && (
        <div
          id="alert-3"
          className="flex items-center justify-center p-4 mb-4 text-green-800 rounded-lg bg-green-50 dark:bg-gray-800 dark:text-green-400"
          role="alert"
        >
          <span>Teacher Added</span>
        </div>
      )}
    </form>
  )
}

export default MadeForm
