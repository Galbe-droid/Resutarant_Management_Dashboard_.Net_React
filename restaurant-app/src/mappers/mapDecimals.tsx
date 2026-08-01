export function mapDecimals(decimals: number | null) {
    if(!decimals) return "-";
    return new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(decimals)
}