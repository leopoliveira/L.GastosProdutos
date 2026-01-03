'use client';

import React, { useMemo, useState } from 'react';
import { ChevronUp, ChevronDown } from 'lucide-react';
import clsx from 'clsx';

export type ColumnDef<T> = {
  header: string;
  key?: keyof T;
  sortable?: boolean;
  render?: (row: T) => React.ReactNode;
};

type DataGridProps<T> = {
  title: string;
  data: T[];
  columns: ColumnDef<T>[];
  actionsHeader?: string;
  actionsRenderer?: (row: T) => React.ReactNode;
  onAdd?: () => void;
  addLabel?: string;
  filterPlaceholder?: string;
  getFilterValue?: (row: T) => string;
};

export function DataGrid<T extends object>({
  title,
  data,
  columns,
  actionsHeader = 'Ações',
  actionsRenderer,
  onAdd,
  addLabel = 'Adicionar',
  filterPlaceholder = 'Filtrar por Nome',
  getFilterValue,
}: DataGridProps<T>) {
  const [filter, setFilter] = useState('');
  const [sort, setSort] = useState<{ key: keyof T | null; direction: 'asc' | 'desc' }>({
    key: null,
    direction: 'asc',
  });

  const filtered = useMemo(() => {
    const lower = filter.toLowerCase();
    if (!lower) return data;
    const getter = getFilterValue || ((row: any) => String(row?.name ?? ''));
    return data.filter((row) => getter(row).toLowerCase().includes(lower));
  }, [data, filter, getFilterValue]);

  const sorted = useMemo(() => {
    if (!sort.key) return filtered;
    const key = sort.key as keyof T;
    const dir = sort.direction;
    return [...filtered].sort((a: any, b: any) => {
      const av = a[key] as unknown as number | string;
      const bv = b[key] as unknown as number | string;
      if (av < bv) return dir === 'asc' ? -1 : 1;
      if (av > bv) return dir === 'asc' ? 1 : -1;
      return 0;
    });
  }, [filtered, sort]);

  const requestSort = (key?: keyof T, sortable?: boolean) => {
    if (!sortable || !key) return;
    setSort((prev) => ({
      key,
      direction: prev.key === key && prev.direction === 'asc' ? 'desc' : 'asc',
    }));
  };

  return (
    <div className="m-2">
      <div className="flex justify-center">
        <h1 className="text-4xl font-bold">{title}</h1>
      </div>

      <div className="flex justify-end mt-4 mb-4">
        {onAdd && (
          <button
            className="border border-teal-500 text-teal-500 hover:bg-teal-100 font-bold py-2 px-4 rounded text-lg transition-colors cursor-pointer"
            onClick={onAdd}
          >
            {addLabel}
          </button>
        )}
      </div>

      <div className="mt-2 mb-6">
        <input
          placeholder={filterPlaceholder}
          value={filter}
          onChange={(e) => setFilter(e.target.value)}
          className="w-full p-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
      </div>

      <div className="overflow-x-auto rounded-lg border border-gray-200 shadow-sm">
        <table className="min-w-full bg-white">
          <thead className="bg-gray-50">
            <tr>
              {columns.map((col, i) => (
                <th
                  key={i}
                  className={clsx(
                    "px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider",
                    col.sortable && "cursor-pointer hover:bg-gray-100 select-none"
                  )}
                  onClick={() => requestSort(col.key, col.sortable)}
                >
                  <div className="flex items-center gap-1">
                    {col.header}
                    {col.sortable && sort.key === col.key && (
                      sort.direction === 'asc' ? <ChevronUp size={14} /> : <ChevronDown size={14} />
                    )}
                  </div>
                </th>
              ))}
              {actionsRenderer && (
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  {actionsHeader}
                </th>
              )}
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-200">
            {sorted.map((row, i) => (
              <tr key={i} className="hover:bg-gray-50 transition-colors">
                {columns.map((col, j) => (
                  <td key={j} className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                    {col.render ? col.render(row) : (row[col.key as keyof T] as React.ReactNode)}
                  </td>
                ))}
                {actionsRenderer && (
                  <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                    {actionsRenderer(row)}
                  </td>
                )}
              </tr>
            ))}
            {sorted.length === 0 && (
              <tr>
                <td
                  colSpan={columns.length + (actionsRenderer ? 1 : 0)}
                  className="px-6 py-4 text-center text-gray-500"
                >
                  Nenhum registro encontrado.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default DataGrid;
