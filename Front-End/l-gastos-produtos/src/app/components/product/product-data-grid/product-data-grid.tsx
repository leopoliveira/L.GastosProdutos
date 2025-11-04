'use client';

import React, { useState } from 'react';
import { Button, useDisclosure, useToast } from '@chakra-ui/react';
import { AddIcon } from '@chakra-ui/icons';
import ProductModal from '../product-form';
import { IReadProduct } from '@/common/interfaces/product/IReadProduct';
import { UnitOfMeasure } from '@/common/enums/unit-of-measure.enum';
import ProductDeleteModal from '../product-delete-modal';
import { formatCurrency } from '@/common/services/utils/utils';
import DataGrid, { ColumnDef } from '@/app/components/shared/data-grid';

type ProductGridProps = {
  products: IReadProduct[];
  onSubmit: React.Dispatch<React.SetStateAction<boolean>>;
};

const ProductGrid: React.FC<ProductGridProps> = ({ products, onSubmit }) => {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [openDeleteModal, setOpenDeleteModal] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<IReadProduct | null>(null);
  const [selectedProductId, setSelectedProductId] = useState<string>('');
  const toast = useToast();

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
      title: `Matéria Prima ${acao} com sucesso!`,
      status: 'success',
      duration: 5000,
      isClosable: true,
    });
    onSubmit(true);
  };

  const handleSubmitDelete = () => {
    setOpenDeleteModal(false);
    setSelectedProductId('');
    toast({
      title: 'Matéria Prima excluída com sucesso!',
      status: 'success',
      duration: 5000,
      isClosable: true,
    });
    onSubmit(true);
  };

  const columns: ColumnDef<IReadProduct>[] = [
    { header: 'Nome', key: 'name', sortable: true },
    { header: 'Quantidade', key: 'quantity', sortable: true },
    { header: 'Preço', sortable: true, render: (r) => formatCurrency(r.price) },
    {
      header: 'Unidade',
      key: 'unitOfMeasure',
      sortable: true,
      render: (r) => UnitOfMeasure[r.unitOfMeasure] ?? '-',
    },
    { header: 'Preço Unitário', sortable: true, render: (r) => formatCurrency(r.unitPrice) },
  ];

  return (
    <>
      <DataGrid<IReadProduct>
        title="Matéria Prima"
        data={products}
        columns={columns}
        onAdd={handleAdd}
        addLabel="Adicionar"
        actionsRenderer={(row) => (
          <>
            <Button size="sm" colorScheme="blue" mr={2} onClick={() => handleEdit(row)}>
              Editar
            </Button>
            <Button size="sm" colorScheme="red" onClick={() => handleDelete(row.id)}>
              Excluir
            </Button>
          </>
        )}
      />

      <ProductModal
        isOpen={isOpen}
        onClose={onClose}
        product={selectedProduct}
        onSubmit={handleSubmit}
      />
      <ProductDeleteModal
        phrase="Deseja realmente excluir esta matéria prima?"
        btnConfirmLabel="Excluir"
        btnCancelLabel="Cancelar"
        productId={selectedProductId}
        isOpen={openDeleteModal}
        onConfirm={handleSubmitDelete}
        onClose={() => setOpenDeleteModal(false)}
      />
    </>
  );
};

export default ProductGrid;
