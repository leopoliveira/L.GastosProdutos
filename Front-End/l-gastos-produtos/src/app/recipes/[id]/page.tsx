'use client';

import RecipeForm from '@/app/components/recipe/recipe-form';
import IReadRecipe from '@/common/interfaces/recipe/IReadRecipe';
import RecipeService from '@/common/services/recipe';
import { useParams } from 'next/navigation';
import { useEffect, useState } from 'react';
import { toast } from 'sonner';
import { AlertCircle } from 'lucide-react';
import Breadcrumb from '@/app/components/shared/Breadcrumb';

export default function FormRecipe() {
  const { id } = useParams();
  const [recipe, setRecipe] = useState<IReadRecipe>();
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const getData = async (id: string) => {
    setIsLoading(true);
    setError(null);
    try {
      const response = await RecipeService.GetRecipeById(id);
      setRecipe(response);
    } catch (err: any) {
      setError(err?.message ?? 'Erro ao carregar a receita.');
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    if (id) {
      getData(id as string);
    }
  }, []);

  const formSubmitCallback = (): void => {
    toast.success('Receita salva com sucesso!');
  };

  return (
    <main>
      <Breadcrumb items={[
        { label: 'Receitas', href: '/recipes' },
        { label: recipe?.name || 'Editar Receita' }
      ]} />
      {isLoading ? (
        <div className="flex justify-center items-center h-screen">
          <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-blue-500"></div>
        </div>
      ) : error ? (
        <div className="p-4">
          <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative mb-4 flex items-center" role="alert">
            <AlertCircle className="w-5 h-5 mr-2" />
            <span className="block sm:inline">{error}</span>
          </div>
          <button 
            onClick={() => id && getData(id as string)}
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
          >
            Tentar novamente
          </button>
        </div>
      ) : !recipe ? (
        <div className="p-8 text-center">
          <p className="text-lg">Receita não encontrada.</p>
        </div>
      ) : (
        <RecipeForm recipe={recipe} onFormSubmit={formSubmitCallback} />
      )}
    </main>
  );
}
