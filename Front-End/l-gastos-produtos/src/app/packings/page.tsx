"use client";

import IReadPacking from "@/common/interfaces/packing/IReadPacking";
import PackingService from "@/common/services/packing";
import { Box, Spinner } from "@chakra-ui/react";
import { useEffect, useState } from "react";
import PackingGrid from "../components/packing/packing-data-grid/packing-data-grid";

export default function Packings() {
  const [packings, setPackings] = useState<IReadPacking[]>([]);
  const [loading, setLoading] = useState(true);
  const [reRender, setReRender] = useState(false);

  const getData = async () => {
    setLoading(true);

    const response = await PackingService.GetAllPackings();

    setPackings(response);
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
        <PackingGrid
          packings={packings}
          onSubmit={setReRender}
        />
      )}
    </main>
  );
}
