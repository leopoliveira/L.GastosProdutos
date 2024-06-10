"use client";

import { useEffect, useState } from "react";
import { Spinner, Box } from "@chakra-ui/react";
import ProductGrid from "../components/product/product-data-grid";
import "./product-page.css";
import { IProduct } from "@/common/interfaces/IProduct";

export default function Products() {
  const [products, setProducts] = useState<IProduct[]>([]);
  const [loading, setLoading] = useState(true);

  const produtTestId = "665a328b443fd9105cee40fa";

  useEffect(() => {
    const getData = async () => {
      try {
        const response = await fetch(
          `${process.env.NEXT_PUBLIC_PRODUCT_API_URL}/${produtTestId}`
        );

        const data: IProduct = await response.json();

        setProducts([data]);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching data:", error);
        setLoading(false);
      }
    };

    getData();
  }, []);

  return (
    <main>
      {loading ? (
        <Box
          display="flex"
          justifyContent="center"
          alignItems="center"
          height="100vh">
          <Spinner size="xl" />
        </Box>
      ) : (
        <ProductGrid products={products} />
      )}
    </main>
  );
}
