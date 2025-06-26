# AI-Driven SOAR Workflows

This folder contains documentation for my SOAR workflows. The tools and platforms used in these workflows include:

- **TheHive 5**  
  A security incident response platform. I use TheHive to manage SIEM alerts and cases, offering a dedicated environment for collaboration between SOC team members.

- **Cortex Analyzers**  
  Part of TheHive ecosystem, Cortex Analyzers are used to analyze observables (IOCs) at scale. Instead of manually querying multiple tools, analyzers allow centralized enrichment and threat intelligence lookups.

- **n8n**  
  A modern, flexible automation platform. I use n8n to connect all SOC components, design SOAR use cases, and integrate AI to enhance analysis efficiency.

- **Shuffle**  
  My previous primary SOAR platform. It's capable and effective, but I prefer n8n due to its speed, flexibility, and growing ecosystem. Shuffle is still used for certain workflows as a dedicated SOAR platform.


## Implemented Workflows

1. **[Automatic Case Ingestion](/deployment/ai_driven-soar/1.workflow#1-case-ingestion.md)**  
   Automatically creates a case in TheHive when SIEM or EDR alerts are received. It also notifies relevant teams via collaboration tools like Slack.

2. **[Semi-Automatic Knowledgebase Enrichment](/deployment/ai_driven-soar/2.knowledgebase-enichment.md)**  
   Handles and stores project-related knowledge in a structured format to enrich language models and improve contextual awareness.

3. **[TheHive RAG Assistant](/deployment/ai_driven-soar/3.rag-assistant-integration.md)**  
   Integrates an AI Assistant directly into TheHive. Combined with the internal knowledgebase, it assists in investigations by providing real-time, context-aware support.


## Documentation Structure

1. **The Challenge**  
   Describes the problem that led to the development of the workflow.

2. **The Idea** *(Optional)*  
   Shares the inspiration or thought process behind the solution.

3. **About the Workflow**  
   Provides an overview of the workflow and its key processes.

4. **Workflow in Detail**  
   A technical breakdown, including configuration steps and node-by-node explanation.

5. **Workflow in Action**  
   Demonstrates the workflow from a user's point of view.

6. **Future Improvements**  
   Outlines potential enhancements and next steps to improve the workflow further.
