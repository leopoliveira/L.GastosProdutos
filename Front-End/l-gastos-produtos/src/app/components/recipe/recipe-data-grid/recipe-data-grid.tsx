'use client';

import React, { useCallback, useEffect, useMemo, useState } from 'react';
import { toast } from 'sonner';
import { ChevronUp, ChevronDown } from 'lucide-react';
import IReadRecipe from '@/common/interfaces/recipe/IReadRecipe';
import { useRouter } from 'next/navigation';
import RecipeDeleteModal from '../recipe-delete-modal';
import { formatCurrency } from '@/common/services/utils/utils';
import clsx from 'clsx';

type RecipeGridProps = {
  recipes: IReadRecipe[];
  onSubmit: React.Dispatch<React.SetStateAction<boolean>>;
};

const RecipeGrid: React.FC<RecipeGridProps> = ({ recipes, onSubmit }) => {
  const [filter, setFilter] = useState('');
  const [sortedData, setSortedData] = useState<IReadRecipe[]>(recipes);
  const [sortConfig, setSortConfig] = useState<{
    key: keyof IReadRecipe | null;
    direction: 'asc' | 'desc';
  }>({
    key: null,
    direction: 'asc',
  });
  const [openDeleteModal, setOpenDeleteModal] = useState(false);
  const [selectedRecipeId, setSelectedRecipeId] = useState<string>('');

  const router = useRouter();

  useEffect(() => {
    setSortedData(recipes);
  }, [recipes]);

  const onFilterChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFilter(e.target.value);
  };

  const filteredData = useMemo(() => {
    const lower = filter.toLowerCase();
    return sortedData.filter((item) => item.name.toLowerCase().includes(lower));
  }, [sortedData, filter]);

  const onSort = (key: keyof IReadRecipe) => {
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

  const handleShow = useCallback(
    (id: string) => {
      router.push(`/recipes/visualize/${id}`);
    },
    [router]
  );

  const handleEdit = useCallback(
    (id: string) => {
      router.push(`/recipes/${id}`);
    },
    [router]
  );

  const handleDelete = useCallback((id: string) => {
    setOpenDeleteModal(true);
    setSelectedRecipeId(id);
  }, []);

  const handleAdd = useCallback(() => {
    router.push('/recipes/new');
  }, [router]);

  const handleSubmitDelete = () => {
    setOpenDeleteModal(false);
    setSelectedRecipeId('');

    toast.success('Receita excluída com sucesso!');
    onSubmit(true);
  };

  return (
    <div className="m-2">
      <div className="flex justify-center">
        <h1 className="text-4xl font-bold">Receitas</h1>
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
              <th
                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider cursor-pointer hover:bg-gray-100 select-none"
                onClick={() => onSort('totalCost')}
              >
                <div className="flex items-center gap-1">
                  Custo Total
                  {sortConfig.key === 'totalCost' && (
                    sortConfig.direction === 'asc' ? <ChevronUp size={14} /> : <ChevronDown size={14} />
                  )}
                </div>
              </th>
              <th
                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider cursor-pointer hover:bg-gray-100 select-none"
                onClick={() => onSort('quantity')}
              >
                <div className="flex items-center gap-1">
                  Qtde. Produzida
                  {sortConfig.key === 'quantity' && (
                    sortConfig.direction === 'asc' ? <ChevronUp size={14} /> : <ChevronDown size={14} />
                  )}
                </div>
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider select-none">
                Custo por Unidade
              </th>
              <th
                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider cursor-pointer hover:bg-gray-100 select-none"
                onClick={() => onSort('sellingValue')}
              >
                <div className="flex items-center gap-1">
                  Preço de Venda da Un.
                  {sortConfig.key === 'sellingValue' && (
                    sortConfig.direction === 'asc' ? <ChevronUp size={14} /> : <ChevronDown size={14} />
                  )}
                </div>
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider select-none">
                Lucro por Un.
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
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{formatCurrency(row.totalCost)}</td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{row.quantity}</td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{formatCurrency(row.totalCost / row.quantity)}</td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{formatCurrency(row.sellingValue)}</td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{formatCurrency(row.sellingValue - (row.totalCost / row.quantity))}</td>
                <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                  <button
                    className="bg-teal-500 hover:bg-teal-600 text-white font-bold py-1 px-3 rounded mr-2 text-sm transition-colors"
                    onClick={() => handleShow(row.id)}
                  >
                    Visualizar
                  </button>
                  <button
                    className="bg-blue-500 hover:bg-blue-600 text-white font-bold py-1 px-3 rounded mr-2 text-sm transition-colors"
                    onClick={() => handleEdit(row.id)}
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
                <td colSpan={7} className="px-6 py-4 text-center text-gray-500">
                  Nenhum registro encontrado.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>

      <RecipeDeleteModal
        phrase="Deseja realmente excluir esta receita?"
        btnConfirmLabel="Excluir"
        btnCancelLabel="Cancelar"
        recipeId={selectedRecipeId}
        isOpen={openDeleteModal}
        onConfirm={handleSubmitDelete}
        onClose={() => setOpenDeleteModal(false)}
      />
    </div>
  );
};

export default RecipeGrid;
