'use client';

import { useEffect, useState } from 'react';
import IReadRecipe from '@/common/interfaces/recipe/IReadRecipe';
import RecipeService from '@/common/services/recipe';
import RecipeGrid from '../components/recipe/recipe-data-grid/recipe-data-grid';
import { AlertCircle, Loader2 } from 'lucide-react';

export default function Recipes() {
  const [recipes, setRecipes] = useState<IReadRecipe[]>([]);
  const [loading, setLoading] = useState(true);
  const [reRender, setReRender] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const getData = async () => {
    setLoading(true);

    try {
      const response = await RecipeService.GetAllRecipes();
      setRecipes(response);
    } catch (err: any) {
      setError(err?.message ?? 'Erro ao carregar receitas.');
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
      {loading ? (
        <div className="flex justify-center items-center h-screen">
          <Loader2 className="w-12 h-12 animate-spin text-blue-500" />
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
        <RecipeGrid recipes={recipes} onSubmit={setReRender} />
      )}
    </main>
  );
}
