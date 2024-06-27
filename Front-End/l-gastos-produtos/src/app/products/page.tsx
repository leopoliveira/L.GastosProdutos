"use client";

import { useEffect, useState } from "react";
import { Spinner, Box } from "@chakra-ui/react";
import ProductGrid from "../components/product/product-data-grid";
import { IReadProduct } from "@/common/interfaces/product/IReadProduct";
import ProductService from "@/common/services/product";

export default function Products() {
  const [products, setProducts] = useState<IReadProduct[]>([]);
  const [loading, setLoading] = useState(true);
  const [reRender, setReRender] = useState(false);

  const getData = async () => {
    setLoading(true);

    const response = await ProductService.GetAllProducts();

    setProducts(response);
    setLoading(false);
  };

  useEffect(() => {
    getData();
  }, []);

  useEffect(() => {
    if (!reRender) {
      return;
    }
    getData();
    setReRender(false);
  }, [reRender]);

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
        <ProductGrid
          products={products}
          onSubmit={() => setReRender(true)}
        />
      )}
    </main>
  );
}
