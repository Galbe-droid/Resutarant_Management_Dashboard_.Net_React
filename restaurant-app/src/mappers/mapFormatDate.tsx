export function mapFormatDate(dateIso?: string | null) {
    if (!dateIso) return "-";

    return new Date(dateIso).toLocaleDateString("pt-br", {
        hour: "2-digit",
        minute: "2-digit",
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
    })
}