'use client';

import { useEffect, useState } from 'react';
import { AlertCircle } from 'lucide-react';
import ProductGrid from '../components/product/product-data-grid';
import { IReadProduct } from '@/common/interfaces/product/IReadProduct';
import ProductService from '@/common/services/product';
import Breadcrumb from '../components/shared/Breadcrumb';

export default function Products() {
  const [products, setProducts] = useState<IReadProduct[]>([]);
  const [loading, setLoading] = useState(true);
  const [reRender, setReRender] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const getData = async () => {
    setLoading(true);

    try {
      const response = await ProductService.GetAllProducts();
      setProducts(response);
    } catch (err: any) {
      setError(err?.message ?? 'Erro ao carregar produtos.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    getData();
    setReRender(false);
  }, []);

  useEffect(() => {
    if (!reRender) {
      return;
    }
    getData();
    setReRender(false);
  }, [reRender]);

  return (
    <main>
      <Breadcrumb items={[{ label: 'Produtos' }]} />
      {loading ? (
        <div className="p-4">
          <div className="flex flex-col gap-4">
            {[...Array(5)].map((_, i) => (
              <div key={i} className="h-8 bg-gray-200 animate-pulse rounded" />
            ))}
          </div>
        </div>
      ) : error ? (
        <div className="p-4">
          <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative flex items-center mb-4">
            <AlertCircle className="w-5 h-5 mr-2" />
            <span>{error}</span>
          </div>
          <button
            onClick={getData}
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
          >
            Tentar novamente
          </button>
        </div>
      ) : (
        <ProductGrid products={products} onSubmit={setReRender} />
      )}
    </main>
  );
}
