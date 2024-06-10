import ProductGrid from "../components/product/product-data-grid";
import "./product-page.css";

const data = [
  {
    id: "1",
    name: "Item 1",
    quantity: 10,
    price: 5,
    unitOfMeasure: 3,
    unitPrice: 0.5,
  },
  {
    id: "2",
    name: "Item 2",
    quantity: 500,
    price: 8.2,
    unitOfMeasure: 2,
    unitPrice: 0.01644,
  },
  {
    id: "3",
    name: "Item 3",
    quantity: 52,
    price: 80,
    unitOfMeasure: 1,
    unitPrice: 1.5384615384615385,
  },
  // Add more data as needed
];

export default function Products() {
  return (
    <main>
      <ProductGrid products={data} />
    </main>
  );
}
