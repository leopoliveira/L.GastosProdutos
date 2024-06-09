import ProductGrid from "../components/product/product-data-grid";
import "./product-page.css";

const data = [
  { id: "1", name: "Item 1", quantity: 10, price: 5, totalCost: 50 },
  { id: "2", name: "Item 2", quantity: 5, price: 8, totalCost: 40 },
  { id: "3", name: "Item 3", quantity: 52, price: 80, totalCost: 410 },
  // Add more data as needed
];

export default function Products() {
  return (
    <main>
      <ProductGrid products={data} />
    </main>
  );
}
