'use client';

import IReadPacking from '@/common/interfaces/packing/IReadPacking';
import PackingService from '@/common/services/packing';
import { Box, Spinner, Alert, AlertIcon, Text, Button } from '@chakra-ui/react';
import { useEffect, useState } from 'react';
import PackingGrid from '../components/packing/packing-data-grid/packing-data-grid';

export default function Packings() {
  const [packings, setPackings] = useState<IReadPacking[]>([]);
  const [loading, setLoading] = useState(true);
  const [reRender, setReRender] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const getData = async () => {
    setLoading(true);

    try {
      const response = await PackingService.GetAllPackings();
      setPackings(response);
    } catch (err: any) {
      setError(err?.message ?? 'Erro ao carregar embalagens.');
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
        <Box display="flex" justifyContent="center" alignItems="center" height="100vh">
          <Spinner size="xl" />
        </Box>
      ) : error ? (
        <Box p={4}>
          <Alert status="error" mb={4}>
            <AlertIcon />
            {error}
          </Alert>
          <Button onClick={getData}>Tentar novamente</Button>
        </Box>
      ) : packings.length === 0 ? (
        <Box p={8} textAlign="center">
          <Text fontSize="lg">Nenhuma embalagem encontrada.</Text>
        </Box>
      ) : (
        <PackingGrid packings={packings} onSubmit={setReRender} />
      )}
    </main>
  );
}
