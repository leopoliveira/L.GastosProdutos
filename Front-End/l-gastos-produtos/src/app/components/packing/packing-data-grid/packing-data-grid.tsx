"use client";

import React, { useState } from "react";
import { toast } from "sonner";
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
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [openDeleteModal, setOpenDeleteModal] = useState(false);
  const [selectedPacking, setSelectedPacking] = useState<IReadPacking | null>(null);
  const [selectedPackingId, setSelectedPackingId] = useState<string>("");

  const onOpen = () => setIsModalOpen(true);
  const onClose = () => setIsModalOpen(false);

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
    toast.success(`Embalagem ${acao} com sucesso!`);
    onSubmit(true);
  };

  const handleSubmitDelete = () => {
    setOpenDeleteModal(false);
    setSelectedPackingId("");
    toast.success("Embalagem excluída com sucesso!");
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

      <PackingModal isOpen={isModalOpen} onClose={onClose} packing={selectedPacking} onSubmit={handleSubmit} />
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

