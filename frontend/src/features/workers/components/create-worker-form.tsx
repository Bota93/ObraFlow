import { isAxiosError } from 'axios'
import { useForm } from 'react-hook-form'
import { Link, useNavigate } from 'react-router-dom'
import { ZodError } from 'zod'
import { useCreateWorker } from '../hooks/use-create-worker'
import {
  createWorkerSchema,
  type CreateWorkerFormValues,
} from '../schemas/create-worker.schema'

export function CreateWorkerForm() {
  const navigate = useNavigate()
  const { mutateAsync, isPending, error } = useCreateWorker()
  const {
    register,
    handleSubmit,
    setError,
    clearErrors,
    formState: { errors },
  } = useForm<CreateWorkerFormValues>({
    defaultValues: {
      name: '',
      role: '',
      phoneNumber: '',
      hourlyRate: 0,
      isActive: true,
    },
  })

  const onSubmit = async (values: CreateWorkerFormValues) => {
    clearErrors()

    const result = createWorkerSchema.safeParse(values)

    if (!result.success) {
      applyZodErrors(result.error)
      return
    }

    try {
      await mutateAsync(result.data)
      navigate('/workers')
    } catch (submitError) {
      if (isAxiosError(submitError) && submitError.response?.status === 400) {
        setError('root', {
          message: 'The worker could not be created. Please review the form fields.',
        })
        return
      }

      setError('root', {
        message: 'The API is unavailable right now. Please try again.',
      })
    }
  }

  const applyZodErrors = (zodError: ZodError<CreateWorkerFormValues>) => {
    zodError.issues.forEach((issue) => {
      const field = issue.path[0]

      if (typeof field === 'string') {
        setError(field as keyof CreateWorkerFormValues, {
          message: issue.message,
        })
      }
    })
  }

  return (
    <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
      <form className="space-y-6" onSubmit={(event) => void handleSubmit(onSubmit)(event)}>
        <div className="grid gap-6 md:grid-cols-2">
          <label className="space-y-2">
            <span className="text-sm font-medium text-slate-700">Name</span>
            <input
              {...register('name')}
              type="text"
              className="w-full rounded-xl border border-slate-300 px-4 py-3 text-sm text-slate-950 outline-none transition focus:border-slate-950"
              placeholder="Adrian Lopez"
              disabled={isPending}
            />
            {errors.name ? (
              <p className="text-sm text-rose-700">{errors.name.message}</p>
            ) : null}
          </label>

          <label className="space-y-2">
            <span className="text-sm font-medium text-slate-700">Role</span>
            <input
              {...register('role')}
              type="text"
              className="w-full rounded-xl border border-slate-300 px-4 py-3 text-sm text-slate-950 outline-none transition focus:border-slate-950"
              placeholder="Electrician"
              disabled={isPending}
            />
            {errors.role ? (
              <p className="text-sm text-rose-700">{errors.role.message}</p>
            ) : null}
          </label>

          <label className="space-y-2">
            <span className="text-sm font-medium text-slate-700">Phone number</span>
            <input
              {...register('phoneNumber')}
              type="text"
              className="w-full rounded-xl border border-slate-300 px-4 py-3 text-sm text-slate-950 outline-none transition focus:border-slate-950"
              placeholder="600123456"
              disabled={isPending}
            />
            {errors.phoneNumber ? (
              <p className="text-sm text-rose-700">{errors.phoneNumber.message}</p>
            ) : null}
          </label>

          <label className="space-y-2">
            <span className="text-sm font-medium text-slate-700">Hourly rate</span>
            <input
              {...register('hourlyRate', { valueAsNumber: true })}
              type="number"
              step="0.01"
              min="0.01"
              className="w-full rounded-xl border border-slate-300 px-4 py-3 text-sm text-slate-950 outline-none transition focus:border-slate-950"
              placeholder="25.50"
              disabled={isPending}
            />
            {errors.hourlyRate ? (
              <p className="text-sm text-rose-700">{errors.hourlyRate.message}</p>
            ) : null}
          </label>
        </div>

        <label className="flex items-center gap-3 rounded-xl border border-slate-200 bg-slate-50 px-4 py-3">
          <input
            {...register('isActive')}
            type="checkbox"
            className="h-4 w-4 rounded border-slate-300 text-slate-950"
            disabled={isPending}
          />
          <span className="text-sm text-slate-700">Worker is active</span>
        </label>

        {errors.root?.message ? (
          <div className="rounded-xl border border-rose-200 bg-rose-50 px-4 py-3 text-sm text-rose-700">
            {errors.root.message}
          </div>
        ) : null}

        {error && !errors.root?.message ? (
          <div className="rounded-xl border border-rose-200 bg-rose-50 px-4 py-3 text-sm text-rose-700">
            {error.message}
          </div>
        ) : null}

        <div className="flex flex-col gap-3 sm:flex-row">
          <button
            type="submit"
            disabled={isPending}
            className="inline-flex items-center justify-center rounded-full bg-slate-950 px-5 py-3 text-sm font-semibold text-white transition hover:bg-slate-800 disabled:cursor-not-allowed disabled:opacity-70"
          >
            {isPending ? 'Creating worker...' : 'Create worker'}
          </button>
          <Link
            to="/workers"
            className="inline-flex items-center justify-center rounded-full border border-slate-300 px-5 py-3 text-sm font-semibold text-slate-700 transition hover:bg-slate-100"
          >
            Cancel
          </Link>
        </div>
      </form>
    </section>
  )
}
