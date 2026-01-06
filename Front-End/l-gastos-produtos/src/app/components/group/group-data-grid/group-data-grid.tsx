'use client';

import React, { useCallback, useEffect, useMemo, useState } from 'react';
import { toast } from 'sonner';
import { ChevronUp, ChevronDown } from 'lucide-react';
import IReadGroup from '@/common/interfaces/group/IReadGroup';
import GroupDeleteModal from '../group-delete-modal';
import GroupFormModal from '../group-form-modal';

type GroupGridProps = {
  groups: IReadGroup[];
  onSubmit: React.Dispatch<React.SetStateAction<boolean>>;
};

const GroupGrid: React.FC<GroupGridProps> = ({ groups, onSubmit }) => {
  const [filter, setFilter] = useState('');
  const [sortedData, setSortedData] = useState<IReadGroup[]>(groups);
  const [sortConfig, setSortConfig] = useState<{
    key: keyof IReadGroup | null;
    direction: 'asc' | 'desc';
  }>({
    key: null,
    direction: 'asc',
  });
  const [openDeleteModal, setOpenDeleteModal] = useState(false);
  const [openFormModal, setOpenFormModal] = useState(false);
  const [selectedGroupId, setSelectedGroupId] = useState<string>('');
  const [editingGroup, setEditingGroup] = useState<IReadGroup | null>(null);

  useEffect(() => {
    setSortedData(groups);
  }, [groups]);

  const onFilterChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFilter(e.target.value);
  };

  const filteredData = useMemo(() => {
    const lower = filter.toLowerCase();
    return sortedData.filter((item) => item.name.toLowerCase().includes(lower));
  }, [sortedData, filter]);

  const onSort = (key: keyof IReadGroup) => {
    const nextDirection: 'asc' | 'desc' =
      sortConfig.key === key && sortConfig.direction === 'asc' ? 'desc' : 'asc';
    const sortedArray = [...sortedData].sort((a, b) => {
      const av = a[key] as unknown as number | string;
      const bv = b[key] as unknown as number | string;
      if (av < bv) return nextDirection === 'asc' ? -1 : 1;
      if (av > bv) return nextDirection === 'asc' ? 1 : -1;
      return 0;
    });
    setSortedData(sortedArray);
    setSortConfig({ key, direction: nextDirection });
  };

  const handleDelete = useCallback((id: string) => {
    setOpenDeleteModal(true);
    setSelectedGroupId(id);
  }, []);

  const handleAdd = useCallback(() => {
    setEditingGroup(null);
    setOpenFormModal(true);
  }, []);

  const handleEdit = useCallback((group: IReadGroup) => {
    setEditingGroup(group);
    setOpenFormModal(true);
  }, []);

  const handleSubmitDelete = () => {
    setOpenDeleteModal(false);
    setSelectedGroupId('');
    toast.success('Grupo excluído com sucesso!');
    onSubmit(true);
  };

  const handleSubmitForm = () => {
    setOpenFormModal(false);
    setEditingGroup(null);
    toast.success(editingGroup ? 'Grupo atualizado com sucesso!' : 'Grupo criado com sucesso!');
    onSubmit(true);
  };

  return (
    <div className="m-2">
      <div className="flex justify-center">
        <h1 className="text-4xl font-bold">Grupos de Receitas</h1>
      </div>

      <div className="flex justify-end mt-4 mb-4">
        <button
          className="border border-teal-500 text-teal-500 hover:bg-teal-100 font-bold py-2 px-4 rounded text-lg transition-colors cursor-pointer"
          onClick={handleAdd}
        >
          Adicionar
        </button>
      </div>

      <div className="mt-2 mb-6">
        <input
          placeholder="Filtrar por Nome"
          value={filter}
          onChange={onFilterChange}
          className="w-full p-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
      </div>

      <div className="overflow-x-auto rounded-lg border border-gray-200 shadow-sm">
        <table className="min-w-full bg-white">
          <thead className="bg-gray-50">
            <tr>
              <th
                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider cursor-pointer hover:bg-gray-100 select-none"
                onClick={() => onSort('name')}
              >
                <div className="flex items-center gap-1">
                  Nome
                  {sortConfig.key === 'name' && (
                    sortConfig.direction === 'asc' ? <ChevronUp size={14} /> : <ChevronDown size={14} />
                  )}
                </div>
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Descrição
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Ações
              </th>
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-200">
            {filteredData.map((row) => (
              <tr key={row.id} className="hover:bg-gray-50 transition-colors">
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{row.name}</td>
                <td className="px-6 py-4 text-sm text-gray-900">{row.description || '-'}</td>
                <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
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
                </td>
              </tr>
            ))}
            {filteredData.length === 0 && (
              <tr>
                <td colSpan={3} className="px-6 py-4 text-center text-gray-500">
                  Nenhum registro encontrado.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>

      <GroupDeleteModal
        phrase="Deseja realmente excluir este grupo?"
        btnConfirmLabel="Excluir"
        btnCancelLabel="Cancelar"
        groupId={selectedGroupId}
        isOpen={openDeleteModal}
        onConfirm={handleSubmitDelete}
        onClose={() => setOpenDeleteModal(false)}
      />

      <GroupFormModal
        group={editingGroup}
        isOpen={openFormModal}
        onConfirm={handleSubmitForm}
        onClose={() => {
          setOpenFormModal(false);
          setEditingGroup(null);
        }}
      />
    </div>
  );
};

export default GroupGrid;
