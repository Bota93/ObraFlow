import { IncidentsPlaceholder } from '../../features/incidents/components/incidents-placeholder'
import { PageHeader } from '../../shared/components/page-header'

export function IncidentsPage() {
  return (
    <div className="space-y-6">
      <PageHeader
        eyebrow="Incidents"
        title="Incident tracking"
        description="Thin page wrapper prepared for incident list, detail, and update flows."
      />
      <IncidentsPlaceholder />
    </div>
  )
}
