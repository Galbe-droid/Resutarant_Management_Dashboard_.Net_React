export interface CreateProductDto {
    name: string;
    price: number;
    description: string;
    imageURL: string;
    categoryId: string;
}