"use client";

import React, { useEffect, useState } from "react";
import {
  Box,
  Button,
  Flex,
  Heading,
  Input,
  Table,
  Tbody,
  Td,
  Th,
  Thead,
  Tr,
  useDisclosure,
  useToast,
} from "@chakra-ui/react";
import { AddIcon } from "@chakra-ui/icons";
import PackingModal from "../packing-form";
import IReadPacking from "@/common/interfaces/packing/IReadPacking";
import { UnitOfMeasure } from "@/common/enums/unit-of-measure.enum";
import PackingDeleteModal from "../packing-delet-modal";
import { formatCurrency } from "@/common/services/utils/utils";

type PackingGridProps = {
  packings: IReadPacking[];
  onSubmit: React.Dispatch<React.SetStateAction<boolean>>;
};

const PackingGrid: React.FC<PackingGridProps> = ({
  packings,
  onSubmit,
}) => {
  const [filter, setFilter] = useState("");
  const [sortedData, setSortedData] =
    useState<IReadPacking[]>(packings);
  const [sortConfig, setSortConfig] = useState({
    key: "",
    direction: "",
  });
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [openDeleteModal, setOpenDeleteModal] = useState(false);
  const [selectedPacking, setSelectedPacking] =
    useState<IReadPacking | null>(null);
  const [selectedPackingId, setSelectedPackingId] =
    useState<string>("");
  const toast = useToast();

  useEffect(() => {
    setSortedData(packings);
  }, [packings]);

  const onFilterChange = (e: any) => {
    setFilter(e.target.value);
  };

  const filteredData = sortedData.filter((item) =>
    item.name.toLowerCase().includes(filter.toLowerCase())
  );

  const onSort = (key: any) => {
    let direction = "ascending";

    if (
      sortConfig.key === key &&
      sortConfig.direction === "ascending"
    ) {
      direction = "descending";
    }

    const sortedArray = [...sortedData].sort((a: any, b: any) => {
      if (a[key] < b[key]) {
        return direction === "ascending" ? -1 : 1;
      }
      if (a[key] > b[key]) {
        return direction === "ascending" ? 1 : -1;
      }
      return 0;
    });

    setSortedData(sortedArray);
    setSortConfig({ key, direction });
  };

  const handleEdit = (packing: IReadPacking) => {
    setSelectedPacking(packing);
    onOpen();
  };

  const handleDelete = (id: string) => {
    setOpenDeleteModal(true);
    setSelectedPackingId(id);
  };

  const handleAdd = () => {
    setSelectedPacking(null);
    onOpen();
  };

  const handleSubmit = (acao: string) => {
    toast({
      title: `Embalagem ${acao} com sucesso!`,
      status: "success",
      duration: 5000,
      isClosable: true,
    });

    onSubmit(true);
  };

  const handleSubmitDelete = () => {
    setOpenDeleteModal(false);
    setSelectedPackingId("");

    toast({
      title: "Embalagem excluída com sucesso!",
      status: "success",
      duration: 5000,
      isClosable: true,
    });

    onSubmit(true);
  };

  return (
    <Box m={2}>
      <Flex justifyContent="center">
        <Heading
          as="h1"
          size="2xl">
          Embalagens
        </Heading>
      </Flex>
      <Flex
        justify="flex-end"
        mt={4}
        mb={4}>
        <Button
          colorScheme="teal"
          size="lg"
          variant="outline"
          leftIcon={<AddIcon />}
          onClick={handleAdd}>
          Adicionar
        </Button>
      </Flex>
      <Flex
        mt={2}
        mb={6}>
        <Input
          placeholder="Filtrar por Nome"
          value={filter}
          onChange={onFilterChange}
        />
      </Flex>
      <Table
        variant="striped"
        width="100%"
        size="lg">
        <Thead>
          <Tr>
            <Th
              cursor="pointer"
              textAlign="center"
              onClick={() => onSort("name")}>
              Nome
            </Th>
            <Th
              cursor="pointer"
              textAlign="center"
              onClick={() => onSort("quantity")}>
              Quantidade
            </Th>
            <Th
              cursor="pointer"
              textAlign="center"
              onClick={() => onSort("price")}>
              Preço
            </Th>
            <Th
              cursor="pointer"
              textAlign="center"
              onClick={() => onSort("unitOfMeasure")}>
              Unidade
            </Th>
            <Th
              cursor="pointer"
              textAlign="center"
              onClick={() => onSort("unitPrice")}>
              Preço Unitário
            </Th>
            <Th
              cursor="pointer"
              textAlign="center">
              Ações
            </Th>
          </Tr>
        </Thead>
        <Tbody>
          {filteredData.map((item) => (
            <Tr key={item.id}>
              <Td display="none">{item.id}</Td>
              <Td textAlign="center">{item.name}</Td>
              <Td textAlign="center">{item.quantity}</Td>
              <Td textAlign="center">{formatCurrency(item.price)}</Td>
              <Td textAlign="center">
                {UnitOfMeasure[item.unitOfMeasure] ?? "-"}
              </Td>
              <Td textAlign="center">
                {formatCurrency(item.price / item.quantity)}
              </Td>
              <Td textAlign="center">
                <Button
                  size="sm"
                  colorScheme="blue"
                  mr={2}
                  onClick={() => handleEdit(item)}>
                  Editar
                </Button>
                <Button
                  size="sm"
                  colorScheme="red"
                  onClick={() => handleDelete(item.id)}>
                  Excluir
                </Button>
              </Td>
            </Tr>
          ))}
        </Tbody>
      </Table>

      <PackingModal
        isOpen={isOpen}
        onClose={onClose}
        packing={selectedPacking}
        onSubmit={handleSubmit}
      />
      <PackingDeleteModal
        phrase="Deseja realmente excluir este embalagem?"
        btnConfirmLabel="Excluir"
        btnCancelLabel="Cancelar"
        packingId={selectedPackingId}
        isOpen={openDeleteModal}
        onConfirm={handleSubmitDelete}
        onClose={() => setOpenDeleteModal(false)}
      />
    </Box>
  );
};

export default PackingGrid;
