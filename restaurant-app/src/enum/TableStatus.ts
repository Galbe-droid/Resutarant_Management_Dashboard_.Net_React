const TableStatus = {
    Avaliable:  0,
    Occupied: 1,
    Reserved: 2
} as const;

type TableStatus = typeof TableStatus[keyof typeof TableStatus];

export { TableStatus };