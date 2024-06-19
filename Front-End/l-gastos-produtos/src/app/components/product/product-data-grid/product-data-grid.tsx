"use client";

import React, { useEffect, useState } from "react";
import {
  Box,
  Button,
  Flex,
  Heading,
  Input,
  Link,
  Table,
  Tbody,
  Td,
  Text,
  Th,
  Thead,
  Tr,
  useDisclosure,
  useToast,
} from "@chakra-ui/react";
import { AddIcon } from "@chakra-ui/icons";
import ProductModal from "../product-form";
import { IReadProduct } from "@/common/interfaces/product/IReadProduct";
import { UnitOfMeasure } from "@/common/enums/unit-of-measure.enum";
import DeleteModal from "../../delete-modal";

type ProductGridProps = {
  products: IReadProduct[];
  onSubmit: React.Dispatch<React.SetStateAction<boolean>>;
};

const ProductGrid: React.FC<ProductGridProps> = ({
  products,
  onSubmit,
}) => {
  const [filter, setFilter] = useState("");
  const [sortedData, setSortedData] =
    useState<IReadProduct[]>(products);
  const [sortConfig, setSortConfig] = useState({
    key: "",
    direction: "",
  });
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [openDeleteModal, setOpenDeleteModal] = useState(false);
  const [selectedProduct, setSelectedProduct] =
    useState<IReadProduct | null>(null);
  const [selectedProductId, setSelectedProductId] =
    useState<string>("");
  const toast = useToast();

  useEffect(() => {
    setSortedData(products);
  }, [products]);

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

  const formatCurrency = (value: number) => {
    return new Intl.NumberFormat("pt-BR", {
      style: "currency",
      currency: "BRL",
      minimumFractionDigits: 2,
      maximumFractionDigits: 2,
    }).format(value);
  };

  const handleEdit = (product: IReadProduct) => {
    setSelectedProduct(product);
    onOpen();
  };

  const handleDelete = (id: string) => {
    setOpenDeleteModal(true);
    setSelectedProductId(id);
  };

  const handleAdd = () => {
    setSelectedProduct(null);
    onOpen();
  };

  const handleSubmit = (acao: string) => {
    toast({
      title: `Produto ${acao} com sucesso!`,
      status: "success",
      duration: 5000,
      isClosable: true,
    });

    onSubmit(true);
  };

  const handleSubmitDelete = () => {
    setOpenDeleteModal(false);
    setSelectedProductId("");

    toast({
      title: "Produto excluído com sucesso!",
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
          Produtos
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
                {formatCurrency(item.unitPrice)}
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

      <ProductModal
        isOpen={isOpen}
        onClose={onClose}
        product={selectedProduct}
        onSubmit={handleSubmit}
      />
      <DeleteModal
        phrase="Deseja realmente excluir este produto?"
        btnConfirmLabel="Excluir"
        btnCancelLabel="Cancelar"
        productId={selectedProductId}
        isOpen={openDeleteModal}
        onConfirm={handleSubmitDelete}
        onClose={() => setOpenDeleteModal(false)}
      />
    </Box>
  );
};

export default ProductGrid;
