import { DailyReportsPlaceholder } from '../../features/daily-reports/components/daily-reports-placeholder'
import { PageHeader } from '../../shared/components/page-header'

export function DailyReportsPage() {
  return (
    <div className="space-y-6">
      <PageHeader
        eyebrow="Daily Reports"
        title="Daily site reporting"
        description="Route-level composition for report listing, details, and future creation workflows."
      />
      <DailyReportsPlaceholder />
    </div>
  )
}
