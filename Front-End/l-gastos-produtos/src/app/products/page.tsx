'use client';

import { useEffect, useState } from 'react';
import { Box, Skeleton, Stack, Alert, AlertIcon, Text, Button } from '@chakra-ui/react';
import ProductGrid from '../components/product/product-data-grid';
import { IReadProduct } from '@/common/interfaces/product/IReadProduct';
import ProductService from '@/common/services/product';

export default function Products() {
  const [products, setProducts] = useState<IReadProduct[]>([]);
  const [loading, setLoading] = useState(true);
  const [reRender, setReRender] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const getData = async () => {
    setLoading(true);

    try {
      const response = await ProductService.GetAllProducts();
      setProducts(response);
    } catch (err: any) {
      setError(err?.message ?? 'Erro ao carregar produtos.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    getData();
    setReRender(false);
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
        <Box p={4}>
          <Stack spacing={4}>
            <Skeleton height="32px" />
            <Skeleton height="32px" />
            <Skeleton height="32px" />
            <Skeleton height="32px" />
            <Skeleton height="32px" />
          </Stack>
        </Box>
      ) : error ? (
        <Box p={4}>
          <Alert status="error" mb={4}>
            <AlertIcon />
            {error}
          </Alert>
          <Button onClick={getData}>Tentar novamente</Button>
        </Box>
      ) : (
        <ProductGrid products={products} onSubmit={setReRender} />
      )}
    </main>
  );
}
