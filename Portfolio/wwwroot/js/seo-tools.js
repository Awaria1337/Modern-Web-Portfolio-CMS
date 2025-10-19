// SEO Analysis Tools
class SeoTools {
    constructor() {
        this.initializeEventListeners();
    }

    initializeEventListeners() {
        // Character counters
        this.setupCharacterCounters();
        
        // SEO analysis buttons
        this.setupSeoAnalysisButtons();
        
        // Slug generators
        this.setupSlugGenerators();
    }

    setupCharacterCounters() {
        // Meta title counter
        const metaTitleInputs = document.querySelectorAll('input[name*="MetaTitle"], input[name*="metaTitle"]');
        metaTitleInputs.forEach(input => {
            this.addCharacterCounter(input, 60, 'title');
        });

        // Meta description counter
        const metaDescInputs = document.querySelectorAll('textarea[name*="MetaDescription"], textarea[name*="metaDescription"]');
        metaDescInputs.forEach(input => {
            this.addCharacterCounter(input, 160, 'description');
        });

        // Meta keywords counter
        const metaKeywordsInputs = document.querySelectorAll('input[name*="MetaKeywords"], input[name*="metaKeywords"]');
        metaKeywordsInputs.forEach(input => {
            this.addCharacterCounter(input, 255, 'keywords');
        });
    }

    addCharacterCounter(input, maxLength, type) {
        const counterId = `${input.id || input.name}-counter`;
        let counter = document.getElementById(counterId);
        
        if (!counter) {
            counter = document.createElement('div');
            counter.id = counterId;
            counter.className = 'character-counter mt-1';
            input.parentNode.appendChild(counter);
        }

        const updateCounter = () => {
            const length = input.value.length;
            const remaining = maxLength - length;
            const percentage = (length / maxLength) * 100;
            
            let colorClass = 'text-success';
            if (percentage > 80) colorClass = 'text-warning';
            if (percentage > 95) colorClass = 'text-danger';
            
            counter.innerHTML = `
                <small class="${colorClass}">
                    ${length}/${maxLength} karakter 
                    ${remaining >= 0 ? `(${remaining} kaldı)` : `(${Math.abs(remaining)} fazla)`}
                </small>
            `;
            
            // Progress bar
            const progressBar = counter.querySelector('.progress') || this.createProgressBar(counter);
            const progressFill = progressBar.querySelector('.progress-bar');
            progressFill.style.width = `${Math.min(percentage, 100)}%`;
            progressFill.className = `progress-bar ${percentage > 95 ? 'bg-danger' : percentage > 80 ? 'bg-warning' : 'bg-success'}`;
        };

        input.addEventListener('input', updateCounter);
        updateCounter(); // Initial call
    }

    createProgressBar(container) {
        const progressBar = document.createElement('div');
        progressBar.className = 'progress mt-1';
        progressBar.style.height = '4px';
        progressBar.innerHTML = '<div class="progress-bar" role="progressbar"></div>';
        container.appendChild(progressBar);
        return progressBar;
    }

    setupSeoAnalysisButtons() {
        const analyzeButtons = document.querySelectorAll('.analyze-seo-btn');
        analyzeButtons.forEach(button => {
            button.addEventListener('click', (e) => {
                e.preventDefault();
                const contentType = button.dataset.contentType;
                const contentId = button.dataset.contentId;
                this.analyzeSeo(contentType, contentId, button);
            });
        });

        // Bulk analysis
        const bulkAnalyzeBtn = document.getElementById('analyze-all-seo');
        if (bulkAnalyzeBtn) {
            bulkAnalyzeBtn.addEventListener('click', (e) => {
                e.preventDefault();
                this.bulkAnalyzeSeo();
            });
        }
    }

    async analyzeSeo(contentType, contentId, button) {
        const originalText = button.innerHTML;
        button.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Analiz ediliyor...';
        button.disabled = true;

        try {
            const response = await fetch('/AdminSeo/AnalyzeSeo', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
                },
                body: `contentType=${contentType}&contentId=${contentId}`
            });

            const result = await response.json();
            
            if (result.success) {
                this.displaySeoResults(result, contentId);
                this.updateSeoScore(contentId, result.score, result.scoreText, result.scoreColor);
            } else {
                this.showError('SEO analizi sırasında bir hata oluştu: ' + result.message);
            }
        } catch (error) {
            this.showError('SEO analizi sırasında bir hata oluştu: ' + error.message);
        } finally {
            button.innerHTML = originalText;
            button.disabled = false;
        }
    }

    async bulkAnalyzeSeo() {
        const analyzeButtons = document.querySelectorAll('.analyze-seo-btn');
        const totalItems = analyzeButtons.length;
        let completed = 0;

        const progressModal = this.createProgressModal(totalItems);
        document.body.appendChild(progressModal);
        
        const modal = new bootstrap.Modal(progressModal);
        modal.show();

        for (const button of analyzeButtons) {
            const contentType = button.dataset.contentType;
            const contentId = button.dataset.contentId;
            
            try {
                await this.analyzeSeo(contentType, contentId, button);
                completed++;
                this.updateProgress(progressModal, completed, totalItems);
                
                // Small delay to prevent overwhelming the server
                await new Promise(resolve => setTimeout(resolve, 500));
            } catch (error) {
                console.error(`Error analyzing ${contentType} ${contentId}:`, error);
            }
        }

        setTimeout(() => {
            modal.hide();
            progressModal.remove();
            this.showSuccess(`${completed} içerik başarıyla analiz edildi.`);
        }, 1000);
    }

    createProgressModal(total) {
        const modal = document.createElement('div');
        modal.className = 'modal fade';
        modal.innerHTML = `
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">SEO Analizi Yapılıyor</h5>
                    </div>
                    <div class="modal-body">
                        <div class="progress mb-3">
                            <div class="progress-bar" role="progressbar" style="width: 0%"></div>
                        </div>
                        <p class="mb-0">
                            <span class="current">0</span> / <span class="total">${total}</span> içerik analiz edildi
                        </p>
                    </div>
                </div>
            </div>
        `;
        return modal;
    }

    updateProgress(modal, current, total) {
        const percentage = (current / total) * 100;
        const progressBar = modal.querySelector('.progress-bar');
        const currentSpan = modal.querySelector('.current');
        
        progressBar.style.width = `${percentage}%`;
        currentSpan.textContent = current;
    }

    displaySeoResults(result, contentId) {
        const modal = document.getElementById('seoAnalysisModal');
        if (!modal) return;

        const modalBody = modal.querySelector('.modal-body');
        modalBody.innerHTML = `
            <div class="seo-analysis-results">
                <div class="row mb-3">
                    <div class="col-md-4">
                        <div class="text-center">
                            <div class="seo-score-circle ${result.scoreColor}">
                                <span class="score">${result.score}</span>
                            </div>
                            <p class="mt-2 mb-0">${result.scoreText}</p>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <h6>SEO Metrikleri</h6>
                        <div class="seo-metrics">
                            ${this.renderMetrics(result.metrics)}
                        </div>
                    </div>
                </div>
                
                ${result.issues && result.issues.length > 0 ? `
                <div class="mb-3">
                    <h6 class="text-warning"><i class="fas fa-exclamation-triangle"></i> Sorunlar</h6>
                    <ul class="list-unstyled">
                        ${result.issues.map(issue => `<li class="text-warning"><i class="fas fa-minus"></i> ${issue}</li>`).join('')}
                    </ul>
                </div>
                ` : ''}
                
                ${result.recommendations && result.recommendations.length > 0 ? `
                <div class="mb-3">
                    <h6 class="text-info"><i class="fas fa-lightbulb"></i> Öneriler</h6>
                    <ul class="list-unstyled">
                        ${result.recommendations.map(rec => `<li class="text-info"><i class="fas fa-plus"></i> ${rec}</li>`).join('')}
                    </ul>
                </div>
                ` : ''}
            </div>
        `;

        const bsModal = new bootstrap.Modal(modal);
        bsModal.show();
    }

    renderMetrics(metrics) {
        if (!metrics) return '';
        
        return Object.entries(metrics).map(([key, value]) => {
            const label = this.getMetricLabel(key);
            return `
                <div class="d-flex justify-content-between">
                    <span>${label}:</span>
                    <strong>${value}</strong>
                </div>
            `;
        }).join('');
    }

    getMetricLabel(key) {
        const labels = {
            titleLength: 'Başlık Uzunluğu',
            descriptionLength: 'Açıklama Uzunluğu',
            keywordDensity: 'Anahtar Kelime Yoğunluğu',
            readabilityScore: 'Okunabilirlik Skoru',
            imageCount: 'Resim Sayısı',
            linkCount: 'Link Sayısı'
        };
        return labels[key] || key;
    }

    updateSeoScore(contentId, score, scoreText, scoreColor) {
        const scoreElement = document.querySelector(`[data-content-id="${contentId}"] .seo-score`);
        if (scoreElement) {
            scoreElement.textContent = score;
            scoreElement.className = `seo-score badge ${scoreColor}`;
        }

        const scoreTextElement = document.querySelector(`[data-content-id="${contentId}"] .seo-score-text`);
        if (scoreTextElement) {
            scoreTextElement.textContent = scoreText;
        }

        // Update last analyzed date
        const lastAnalyzedElement = document.querySelector(`[data-content-id="${contentId}"] .last-analyzed`);
        if (lastAnalyzedElement) {
            lastAnalyzedElement.textContent = new Date().toLocaleDateString('tr-TR');
        }
    }

    setupSlugGenerators() {
        const titleInputs = document.querySelectorAll('input[name*="Baslik"], input[name*="baslik"], input[name*="Title"], input[name*="title"]');
        titleInputs.forEach(input => {
            const slugInput = this.findSlugInput(input);
            if (slugInput) {
                input.addEventListener('input', () => {
                    if (!slugInput.value || slugInput.dataset.autoGenerate !== 'false') {
                        this.generateSlug(input.value, slugInput);
                    }
                });

                // Add manual slug generation button
                this.addSlugGenerateButton(input, slugInput);
            }
        });
    }

    findSlugInput(titleInput) {
        const form = titleInput.closest('form');
        if (!form) return null;
        
        return form.querySelector('input[name*="Slug"], input[name*="slug"]');
    }

    addSlugGenerateButton(titleInput, slugInput) {
        if (slugInput.nextElementSibling?.classList.contains('slug-generate-btn')) return;

        const button = document.createElement('button');
        button.type = 'button';
        button.className = 'btn btn-sm btn-outline-secondary slug-generate-btn mt-1';
        button.innerHTML = '<i class="fas fa-link"></i> Slug Oluştur';
        
        button.addEventListener('click', () => {
            this.generateSlug(titleInput.value, slugInput);
            slugInput.dataset.autoGenerate = 'false';
        });

        slugInput.parentNode.appendChild(button);
    }

    generateSlug(title, slugInput) {
        if (!title) return;

        const slug = title
            .toLowerCase()
            .replace(/ğ/g, 'g')
            .replace(/ü/g, 'u')
            .replace(/ş/g, 's')
            .replace(/ı/g, 'i')
            .replace(/ö/g, 'o')
            .replace(/ç/g, 'c')
            .replace(/[^a-z0-9\s-]/g, '')
            .replace(/\s+/g, '-')
            .replace(/-+/g, '-')
            .replace(/^-|-$/g, '');

        slugInput.value = slug;
    }

    showSuccess(message) {
        this.showToast(message, 'success');
    }

    showError(message) {
        this.showToast(message, 'danger');
    }

    showToast(message, type) {
        const toastContainer = document.getElementById('toast-container') || this.createToastContainer();
        
        const toast = document.createElement('div');
        toast.className = `toast align-items-center text-white bg-${type} border-0`;
        toast.setAttribute('role', 'alert');
        toast.innerHTML = `
            <div class="d-flex">
                <div class="toast-body">${message}</div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        `;

        toastContainer.appendChild(toast);
        
        const bsToast = new bootstrap.Toast(toast);
        bsToast.show();
        
        toast.addEventListener('hidden.bs.toast', () => {
            toast.remove();
        });
    }

    createToastContainer() {
        const container = document.createElement('div');
        container.id = 'toast-container';
        container.className = 'toast-container position-fixed top-0 end-0 p-3';
        container.style.zIndex = '1055';
        document.body.appendChild(container);
        return container;
    }
}

// Initialize SEO Tools when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    new SeoTools();
});

// CSS for SEO tools
const seoToolsCSS = `
.character-counter {
    font-size: 0.875rem;
}

.seo-score-circle {
    width: 80px;
    height: 80px;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
    margin: 0 auto;
    font-size: 1.5rem;
    font-weight: bold;
    color: white;
}

.seo-score-circle.bg-success {
    background-color: #28a745 !important;
}

.seo-score-circle.bg-warning {
    background-color: #ffc107 !important;
    color: #212529 !important;
}

.seo-score-circle.bg-danger {
    background-color: #dc3545 !important;
}

.seo-metrics {
    font-size: 0.9rem;
}

.seo-analysis-results .fas {
    width: 16px;
    text-align: center;
}

.slug-generate-btn {
    font-size: 0.8rem;
}
`;

// Inject CSS
const style = document.createElement('style');
style.textContent = seoToolsCSS;
document.head.appendChild(style);