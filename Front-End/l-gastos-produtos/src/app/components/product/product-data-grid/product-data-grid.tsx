'use client';

import React, { useState } from 'react';
import { toast } from 'sonner';
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
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [openDeleteModal, setOpenDeleteModal] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<IReadProduct | null>(null);
  const [selectedProductId, setSelectedProductId] = useState<string>('');

  const onOpen = () => setIsModalOpen(true);
  const onClose = () => setIsModalOpen(false);

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
    toast.success(`Matéria Prima ${acao} com sucesso!`);
    onSubmit(true);
  };

  const handleSubmitDelete = () => {
    setOpenDeleteModal(false);
    setSelectedProductId('');
    toast.success('Matéria Prima excluída com sucesso!');
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
            <button
              className="bg-blue-500 hover:bg-blue-600 text-white font-bold py-1 px-3 rounded mr-2 text-sm transition-colors"
              onClick={() => handleEdit(row)}
            >
              Editar
            </button>
            <button
              className="bg-red-500 hover:bg-red-600 text-white font-bold py-1 px-3 rounded text-sm transition-colors"
              onClick={() => handleDelete(row.id)}
            >
              Excluir
            </button>
          </>
        )}
      />

      <ProductModal
        isOpen={isModalOpen}
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
