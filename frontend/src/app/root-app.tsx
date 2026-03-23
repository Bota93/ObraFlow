import { RouterProvider } from 'react-router-dom'
import { AppProviders } from './providers/app-providers'
import { router } from './router/router'

export function RootApp() {
  return (
    <AppProviders>
      <RouterProvider router={router} />
    </AppProviders>
  )
}
