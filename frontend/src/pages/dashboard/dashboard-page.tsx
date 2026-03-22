import { DashboardOverview } from '../../features/dashboard/components/dashboard-overview'
import { PageHeader } from '../../shared/components/page-header'

export function DashboardPage() {
  return (
    <div className="space-y-6">
      <PageHeader
        eyebrow="Dashboard"
        title="Site activity overview"
        description="Initial route scaffold for summary metrics and quick operational visibility."
      />
      <DashboardOverview />
    </div>
  )
}
