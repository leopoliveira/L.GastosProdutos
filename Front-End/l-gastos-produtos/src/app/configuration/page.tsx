'use client';

import Link from 'next/link';
import Breadcrumb from '../components/shared/Breadcrumb';

export default function Configuration() {
  return (
    <main>
      <Breadcrumb items={[{ label: 'Configurações' }]} />
      <div className="p-4">
        <h1 className="text-2xl font-bold mb-4">Configurações</h1>
        <div className="space-y-4">
          <Link href="/configuration/groups">
            <div className="p-4 bg-gray-100 rounded border border-gray-300 hover:bg-gray-200 cursor-pointer transition-colors">
              <h2 className="text-lg font-semibold">Grupos de Receitas</h2>
              <p className="text-gray-600">Gerencie os grupos de receitas disponíveis no sistema.</p>
            </div>
          </Link>
        </div>
      </div>
    </main>
  );
}
