import { UnitOfMeasure } from "@/common/enums/unit-of-measure.enum"

type IReadProduct = {
  id: string,
  name: string,
  quantity: number,
  price: number,
  unitOfMeasure: UnitOfMeasure,
  unitPrice: number
}