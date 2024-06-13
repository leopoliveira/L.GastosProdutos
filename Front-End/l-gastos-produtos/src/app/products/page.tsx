"use client";

import { useEffect, useState } from "react";
import { Spinner, Box } from "@chakra-ui/react";
import ProductGrid from "../components/product/product-data-grid";
import "./product-page.css";
import { IProduct } from "@/common/interfaces/IProduct";
import ProductService from "@/common/services/product";

export default function Products() {
  const [products, setProducts] = useState<IProduct[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const getData = async () => {
      const response = await ProductService.GetAllProducts();

      setProducts(response);
      setLoading(false);
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
