import { createBrowserRouter, Navigate } from 'react-router-dom'
import { AppLayout } from '../layouts/app-layout'
import { PublicLayout } from '../layouts/public-layout'
import { DashboardPage } from '../../pages/dashboard/dashboard-page'
import { DailyReportsPage } from '../../pages/daily-reports/daily-reports-page'
import { IncidentsPage } from '../../pages/incidents/incidents-page'
import { NotFoundPage } from '../../pages/not-found/not-found-page'
import { WorkersPage } from '../../pages/workers/workers-page'

export const router = createBrowserRouter([
  {
    element: <PublicLayout />,
    children: [
      {
        path: '/',
        element: <Navigate to="/dashboard" replace />,
      },
    ],
  },
  {
    element: <AppLayout />,
    children: [
      {
        path: '/dashboard',
        element: <DashboardPage />,
      },
      {
        path: '/workers',
        element: <WorkersPage />,
      },
      {
        path: '/daily-reports',
        element: <DailyReportsPage />,
      },
      {
        path: '/incidents',
        element: <IncidentsPage />,
      },
    ],
  },
  {
    path: '*',
    element: <NotFoundPage />,
  },
])
