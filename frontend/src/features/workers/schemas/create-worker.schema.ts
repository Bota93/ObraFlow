import { z } from 'zod'

export const createWorkerSchema = z.object({
  name: z
    .string()
    .trim()
    .min(3, 'Name must contain at least 3 characters.')
    .max(140, 'Name must contain at most 140 characters.'),
  role: z
    .string()
    .trim()
    .min(2, 'Role must contain at least 2 characters.')
    .max(80, 'Role must contain at most 80 characters.'),
  phoneNumber: z
    .string()
    .trim()
    .min(7, 'Phone number must contain at least 7 characters.')
    .max(30, 'Phone number must contain at most 30 characters.'),
  hourlyRate: z
    .number({ error: 'Hourly rate is required.' })
    .min(0.01, 'Hourly rate must be greater than 0.')
    .max(999999.99, 'Hourly rate must be lower than 999999.99.'),
  isActive: z.boolean(),
})

export type CreateWorkerFormValues = z.infer<typeof createWorkerSchema>
