"use client";

import React, { useState } from "react";
import { Button, useDisclosure, useToast } from "@chakra-ui/react";
import { AddIcon } from "@chakra-ui/icons";
import PackingModal from "../packing-form";
import IReadPacking from "@/common/interfaces/packing/IReadPacking";
import { UnitOfMeasure } from "@/common/enums/unit-of-measure.enum";
import PackingDeleteModal from "../packing-delet-modal";
import { formatCurrency } from "@/common/services/utils/utils";
import DataGrid, { ColumnDef } from "@/app/components/shared/data-grid";

type PackingGridProps = {
  packings: IReadPacking[];
  onSubmit: React.Dispatch<React.SetStateAction<boolean>>;
};

const PackingGrid: React.FC<PackingGridProps> = ({ packings, onSubmit }) => {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [openDeleteModal, setOpenDeleteModal] = useState(false);
  const [selectedPacking, setSelectedPacking] = useState<IReadPacking | null>(null);
  const [selectedPackingId, setSelectedPackingId] = useState<string>("");
  const toast = useToast();

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

  const columns: ColumnDef<IReadPacking>[] = [
    { header: "Nome", key: "name", sortable: true },
    { header: "Quantidade", key: "quantity", sortable: true },
    { header: "Preço", sortable: true, render: (r) => formatCurrency(r.price) },
    { header: "Unidade", key: "unitOfMeasure", sortable: true, render: (r) => UnitOfMeasure[r.unitOfMeasure] ?? "-" },
    { header: "Preço Unitário", sortable: true, render: (r) => formatCurrency(r.price / r.quantity) },
  ];

  return (
    <>
      <DataGrid<IReadPacking>
        title="Embalagens"
        data={packings}
        columns={columns}
        onAdd={handleAdd}
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

      <PackingModal isOpen={isOpen} onClose={onClose} packing={selectedPacking} onSubmit={handleSubmit} />
      <PackingDeleteModal
        phrase="Deseja realmente excluir este embalagem?"
        btnConfirmLabel="Excluir"
        btnCancelLabel="Cancelar"
        packingId={selectedPackingId}
        isOpen={openDeleteModal}
        onConfirm={handleSubmitDelete}
        onClose={() => setOpenDeleteModal(false)}
      />
    </>
  );
};

export default PackingGrid;

