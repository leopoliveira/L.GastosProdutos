'use client';

import React, { useMemo, useState } from 'react';
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
} from '@chakra-ui/react';

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
    <Box m={2}>
      <Flex justifyContent="center">
        <Heading as="h1" size="2xl">
          {title}
        </Heading>
      </Flex>

      <Flex justify="flex-end" mt={4} mb={4}>
        {onAdd && (
          <Button colorScheme="teal" size="lg" variant="outline" onClick={onAdd}>
            {addLabel}
          </Button>
        )}
      </Flex>

      <Flex mt={2} mb={6}>
        <Input
          placeholder={filterPlaceholder}
          value={filter}
          onChange={(e) => setFilter(e.target.value)}
        />
      </Flex>

      <Box overflowX="auto">
        <Table variant="striped" width="100%" size="lg">
          <Thead>
            <Tr>
              {columns.map((col, idx) => (
                <Th
                  key={idx}
                  cursor={col.sortable && col.key ? 'pointer' : 'default'}
                  textAlign="center"
                  onClick={() => requestSort(col.key, col.sortable)}
                >
                  {col.header}
                </Th>
              ))}
              {actionsRenderer && <Th textAlign="center">{actionsHeader}</Th>}
            </Tr>
          </Thead>
          <Tbody>
            {sorted.map((row, i) => (
              <Tr key={i}>
                {columns.map((col, idx) => (
                  <Td key={idx} textAlign="center">
                    {col.render ? col.render(row) : col.key ? String((row as any)[col.key]) : null}
                  </Td>
                ))}
                {actionsRenderer && <Td textAlign="center">{actionsRenderer(row)}</Td>}
              </Tr>
            ))}
          </Tbody>
        </Table>
      </Box>
    </Box>
  );
}

export default DataGrid;
