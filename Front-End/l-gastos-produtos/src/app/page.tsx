import Link from 'next/link';

export default function Home() {
  return (
    <main className="container mx-auto p-8">
      <div className="max-w-4xl mx-auto">
        <h1 className="text-4xl font-bold mb-6">Gestão de Gastos com Produtos</h1>
        
        <p className="text-lg mb-8">
          Sistema para gerenciar produtos, receitas e embalagens com controle de custos.
        </p>

        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          <Link href="/products" className="p-6 border rounded-lg hover:shadow-lg transition-shadow">
            <h2 className="text-2xl font-semibold mb-2">🛍️ Produtos</h2>
            <p className="text-gray-600">Gerencie seus produtos e acompanhe custos individuais.</p>
          </Link>

          <Link href="/recipes" className="p-6 border rounded-lg hover:shadow-lg transition-shadow">
            <h2 className="text-2xl font-semibold mb-2">📋 Receitas</h2>
            <p className="text-gray-600">Crie e gerencie receitas com ingredientes e proporções.</p>
          </Link>

          <Link href="/packings" className="p-6 border rounded-lg hover:shadow-lg transition-shadow">
            <h2 className="text-2xl font-semibold mb-2">📦 Embalagens</h2>
            <p className="text-gray-600">Gerencie tipos de embalagens e custos associados.</p>
          </Link>
        </div>
      </div>
    </main>
  );
}
