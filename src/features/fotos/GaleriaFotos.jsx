import { useAuth } from '../../context/AuthContext';
import { FileImage, Download, Star, Maximize2 } from 'lucide-react'; // Importamos Maximize2

export default function GaleriaFotos({ fotos, tipo, inmuebleId, onManage }) {
  const { strategy } = useAuth();

  if (fotos.length === 0) {
    return (
        <div className="bg-white p-8 rounded-xl border border-dashed border-gray-300 text-center flex flex-col items-center justify-center h-full">
          <div className="bg-gray-50 p-4 rounded-full mb-3">
              <FileImage size={24} className="text-gray-400" />
          </div>
          <p className="text-gray-500 font-medium">Sin contenido</p>
          <p className="text-xs text-gray-400 mt-1">
              {tipo === 'RAW' ? 'El fotógrafo aún no ha subido archivos.' : 'El grafista no ha entregado ediciones.'}
          </p>
        </div>
      );
  }

  if (tipo === 'RAW') {
     return (
        <div className="bg-white rounded-xl shadow-sm border border-gray-100 overflow-hidden">
          {/* Cabecera compacta */}
          <div className="px-4 py-3 border-b border-gray-100 bg-gray-50 flex justify-between items-center">
              <h3 className="text-sm font-bold text-gray-700 flex items-center gap-2">
                  Archivos RAW <span className="bg-white border px-1.5 rounded text-xs">{fotos.length}</span>
              </h3>
              {strategy?.canUploadEdit && (
                  <a 
                      href={`https://localhost:7282/api/Foto/descargar-zip/${inmuebleId}`}
                      className="text-blue-600 hover:text-blue-800 text-xs font-bold flex items-center gap-1 hover:underline"
                      target="_blank" rel="noreferrer"
                  >
                      <Download size={14} /> ZIP
                  </a>
              )}
          </div>
          
          {/* Lista Scrollable */}
          <div className="max-h-[500px] overflow-y-auto p-2 space-y-1">
              {fotos.map(foto => (
                  <div key={foto.id} className="group flex items-center gap-3 p-2 hover:bg-blue-50 rounded-lg transition-colors cursor-default">
                      <div className="w-8 h-8 bg-blue-100 text-blue-600 rounded flex items-center justify-center flex-shrink-0">
                          <span className="text-[10px] font-black">RAW</span>
                      </div>
                      <div className="min-w-0 flex-1">
                          <p className="text-xs font-medium text-gray-700 truncate">{foto.nombreArchivo}</p>
                          <p className="text-[10px] text-gray-400">{(Math.random() * (15 - 5) + 5).toFixed(1)} MB</p>
                      </div>
                  </div>
              ))}
          </div>
        </div>
      );
  }

  return (
    <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 gap-3">
      {fotos.map(foto => (
        <div 
            key={foto.id} 
            className="bg-white rounded-lg shadow-sm border border-gray-200 overflow-hidden group hover:shadow-md transition-all duration-300 cursor-pointer relative"
            onClick={() => onManage(foto)}
        >
          
          {/* Contenedor Imagen */}
          <div className="h-65 bg-gray-100 relative flex items-center justify-center p-1">
             <img 
                src={`https://localhost:7282/api/Foto/img/${foto.id}`} 
                className="max-h-full max-w-full object-contain drop-shadow-sm transition-transform duration-300 group-hover:scale-105"
                alt="Foto"
                onError={(e) => { e.target.src = 'https://via.placeholder.com/150?text=Error'; }}
             />
             
             {/* Estrella Favorita */}
             {foto.esFavorita && (
                 <div className="absolute top-1.5 right-1.5 bg-yellow-400 text-white p-1.5 rounded-full shadow-md z-10">
                    <Star size={16} fill="white" strokeWidth={3} />
                 </div>
             )}

             {/* Icono de "Ver Grande" al pasar el mouse */}
             <div className="absolute inset-0 bg-black/0 group-hover:bg-black/10 transition-colors flex items-center justify-center">
                 <Maximize2 className="text-white opacity-0 group-hover:opacity-100 transition-opacity drop-shadow-md" size={24} />
             </div>
          </div>
          
          {/* Footer */}
          <div className="p-2">
             <div className="flex justify-between items-center mb-1.5">
                 <span className={`text-[9px] px-1.5 py-0.5 rounded font-bold uppercase tracking-wide truncate max-w-[100%] ${
                    foto.nombreAmbiente !== "Sin clasificar" 
                    ? 'bg-blue-50 text-blue-600 border border-blue-100' 
                    : 'bg-red-50 text-red-500 border border-red-100'
                 }`}>
                    {foto.nombreAmbiente}
                 </span>
             </div>
             <span className="text-[9px] text-gray-400 truncate block">
                {foto.nombreArchivo}
             </span>
          </div>
        </div>
      ))}
    </div>
  );
}